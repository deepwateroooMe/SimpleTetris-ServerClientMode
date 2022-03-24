using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySquare.Blocks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Sockets;
using MySquare.Utilities;
using System.Net;
using MySquare.LanGame;

namespace MySquare
{
    public partial class MainPad : Form
    {
        private GameEngine gameEngine = GameEngine.Instance;
        private byte[] player2Bytes = null;
        private JoinLanGame joinGame = null;
        private Dictionary<MagicEnum, Label> dicMagics = new Dictionary<MagicEnum, Label>();
        private BaseBlock nextBlock = null;
        private SplashImg gameOverSplash = null;
        private SplashImg winSplash = null;
        private SplashString player2ScoreSplash = null;
        private SplashString scoreSplash = null;

        public MainPad()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowLanPad(false);
            InitGameEngineEvent();
            scoreSplash = new SplashString(scoreCanvas, "0", new System.Drawing.Size(40, 40));
            player2ScoreSplash = new SplashString(player2ScoreCanvas, "0", new System.Drawing.Size(40, 40));
            InitMagicLabels();
            gameEngine.MagicPower = 0;
        }

        private void InitMagicLabels()
        {
            dicMagics.Add(MagicEnum.REMOVE_LAST_LINE, magicRemoveLastLine);
            dicMagics.Add(MagicEnum.GROW, magicGrow);
            dicMagics.Add(MagicEnum.SPEED_UP, magicSpeedUp);
            dicMagics.Add(MagicEnum.SPEED_DOWN, magicSpeedDown);
            dicMagics.Add(MagicEnum.RANDOM_BLOCK, magicRandomBlock);
        }

        private void ShowLanPad(bool show)
        {
            this.SafeInvoke(new Action(() =>
            {
                if (show)
                {
                    this.ClientSize = new System.Drawing.Size(mainContainer.Width + panelRight.Width + player2View.Width + labelVs.Width + 30 + 20, mainContainer.Height + menuStrip1.Height + statusStrip1.Height + 10);
                }
                else
                {
                    this.ClientSize = new System.Drawing.Size(mainContainer.Width + panelRight.Width + 30, mainContainer.Height + menuStrip1.Height + statusStrip1.Height + 10);
                }
            }));
        }

        private void InitGameEngineEvent()
        {
            gameEngine.SelfGameOverEvent += new Action(SelfGameOverEvent);
            gameEngine.GameStartEvent += new Action(Instance_GameStartEvent);
            gameEngine.InvalidateFieldEvent += new Action(Instance_InvalidateFieldEvent);
            gameEngine.LineCompleteEvent += new Action(gameEngine_LineCompleteEvent);
            gameEngine.NewBlockEvent += new Action<BaseBlock>(gameEngine_NewBlockEvent);
            gameEngine.LevelUpEvent += new Action<int>(gameEngine_LevelUpEvent);
            gameEngine.MagicChangedEvent += new Action<int>(gameEngine_MagicChangedEvent);
        }

        void gameEngine_MagicChangedEvent(int obj)
        {
            if (obj < progressBar1.Minimum || obj > progressBar1.Maximum)
                return;
            progressBar1.SafeInvoke(new Action(() =>
            {
                progressBar1.Value = obj;
            }));
            FixMagicLabelColor();
        }

        private void FixMagicLabelColor()
        {
            foreach (var d in dicMagics)
            {
                //单机模式下，给对手使用的道具禁用
                if (GameEngine.isRPG && !gameEngine.dicMagic[d.Key].IsLocal)
                {
                    dicMagics[d.Key].ForeColor = Color.Silver;
                    continue;
                }

                bool r = gameEngine.dicMagic[d.Key].CanAfford();
                var c = gameEngine.dicMagic[d.Key].IsLocal ? Color.Lime : Color.LightCoral;
                dicMagics[d.Key].ForeColor = r ? c : Color.Silver;
            }
        }

        void gameEngine_LevelUpEvent(int obj)
        {
            this.SafeInvoke(new Action<int>((l) =>
            {
                labelLevel.Text = l.ToString();
                if (l == 0)
                    scoreSplash.Content = "0";
            }), obj);
        }
        
        void gameEngine_NewBlockEvent(BaseBlock obj)
        {
            nextBlock = obj.Clone() as BaseBlock;
            nextBlock.SetOutterPosition(0, 0);
            nextPreviewContainer.Invalidate();
        }
        
        void gameEngine_LineCompleteEvent()
        {
            this.SafeInvoke(new Action(() => {
                scoreSplash.Content = gameEngine.Score.ToString();
            }));
        }

        void Instance_InvalidateFieldEvent()
        {
            mainContainer.Invalidate();
        }

        void Instance_GameStartEvent()
        {
            gameStatus.Text = "游戏开始";
        }

        void SelfGameOverEvent()
        {
            this.SafeInvoke(new Action(() => {
                mainContainer.Invalidate();
                gameStatus.Text = "己方失败";
                gameOverSplash = new SplashImg(mainContainer, global::MySquare.Properties.Resources.lose, new Size(148, 148));
            }));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    gameStatus.Text = gameEngine.PauseGame();
                    break;
                case Keys.Right:
                    if (gameEngine.CurrentBlock != null)
                        gameEngine.CurrentBlock.MoveRight();
                    break;
                case Keys.Left:
                    if (gameEngine.CurrentBlock != null)
                        gameEngine.CurrentBlock.MoveLeft();
                    break;
                case Keys.Down:
                    if (gameEngine.CurrentBlock != null)
                        gameEngine.CurrentBlock.MoveDown();
                    break;
                case Keys.Up:
                    if (gameEngine.CurrentBlock != null)
                        gameEngine.CurrentBlock.Rotate();
                    break;
                case Keys.S:
                    RestartGame();
                    break;
                case Keys.C:
                    //winSplash = new SplashImg(mainContainer, global::MySquare.Properties.Resources.win, new Size(128, 128));
                    break;
                case Keys.D:
                    gameEngine.PrepareNextBlock(new RandomBlock());
                    break;
                case Keys.F:
                    GameEngine.Instance.RemoveLastLine();
                    break;
                case Keys.V:
                    GameEngine.Instance.MagicPower = 85;
                    break;
                case Keys.D1:
                    PerformMagic(MagicEnum.REMOVE_LAST_LINE);
                    break;
                case Keys.D2:
                    PerformMagic(MagicEnum.GROW);
                    break;
                case Keys.D3:
                    PerformMagic(MagicEnum.SPEED_UP);
                    break;
                case Keys.D4:
                    PerformMagic(MagicEnum.SPEED_DOWN);
                    break;
                case Keys.D5:
                    PerformMagic(MagicEnum.RANDOM_BLOCK);
                    break;
            }
            mainContainer.Invalidate();
        }
       
        private void PerformMagic(MagicEnum m)
        {
            gameEngine.PerformMagic(m);
            FixMagicLabelColor();
        }
        private void mainContainer_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                gameEngine.DrawAllSquares(e.Graphics);
            }
            catch(Exception me)
            {
                MessageBox.Show(me.Message);
            }
        }

        private void 开始新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameEngine.isRPG = true;
            ShowLanPad(false);
            RestartGame();
        }
        
        private void RestartGame()
        {
            gameEngine.RestartGame();
            if (gameOverSplash != null)
                gameOverSplash.Clear();
            if (winSplash != null)
                winSplash.Clear();
        }
        private void 暂停继续ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameStatus.Text = gameEngine.PauseGame();
        }

        private void MainPad_Deactivate(object sender, EventArgs e)
        {
            //if (GameEngine.isRPG)
            //    gameStatus.Text = gameEngine.PauseGame();
        }
        
        private void 建立主机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostOrClientStatus.Text = "主机";
            GameEngine.isRPG = false;
            CreateLanGame createGame = new CreateLanGame();
            createGame.GameStartEvent += new Action(createGame_GameStartEvent);
            LanServer.Instance.RemoteOffLineEvent += new Action<IPEndPoint>(server_ClientOffLineEvent);
            LanServer.Instance.SquaresReceivedEvent += new Action<byte[]>(server_ServerSquaresReceivedEvent);
            LanServer.Instance.RemoteGameOverEvent += new Action<int>(RemoteGameOverEvent);
            LanServer.Instance.RemoteScoreChangedEvent += new Action<int>(Instance_RemoteScoreChangedEvent);
            LanServer.Instance.MagicToolReceivedEvent += new Action<MagicEnum>(Instance_MagicToolReceivedEvent);
            createGame.ShowDialog();
        }

        void Instance_RemoteScoreChangedEvent(int obj)
        {
            this.SafeInvoke(new Action(() =>
            {
                player2ScoreSplash.Content = obj.ToString();
            }));
        }

        void Instance_MagicToolReceivedEvent(MagicEnum obj)
        {
            switch (obj)
            {
                case MagicEnum.GROW:
                    gameEngine.Grow();
                    break;
                case MagicEnum.SPEED_UP:
                    gameEngine.ChangeSpeed(true);
                    break;
                case MagicEnum.RANDOM_BLOCK:
                    gameEngine.PrepareNextBlock(new RandomBlock());
                    break;
            }
        }

        void server_ClientOffLineEvent(IPEndPoint playerEndPoint)
        {
            Player2OffLine(playerEndPoint);
        }

        private void Player2OffLine(IPEndPoint playerEndPoint)
        {
            labelVs.SafeInvoke(new Action(() =>
            {
                labelVs.ForeColor = Color.Gray;
                gameStatus.Text = "【" + playerEndPoint.ToString() + "】离开了游戏";

            }));
        }

        void RemoteGameOverEvent(int arg)
        {
            gameEngine.IsSelfGameOver = true;
            gameStatus.Text = "对方失败";
            winSplash = new SplashImg(mainContainer, global::MySquare.Properties.Resources.win, new Size(128, 128));
            mainContainer.Invalidate();
        }
        
        void createGame_GameStartEvent()
        {
            gameEngine.RestartGame();
            ShowLanPad(true);
        }
        private void InvalidatePlayer2View(byte[] buf)
        {
            player2Bytes = buf;
            player2View.Invalidate();
        }
        void server_ServerSquaresReceivedEvent(byte[] obj)
        {
            InvalidatePlayer2View(obj);
        }
        
        private void 加入游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostOrClientStatus.Text = "客户机";
            GameEngine.isRPG = false;
            LanClient.Instance.RemoteOffLineEvent += new Action<IPEndPoint>(client_ServerOffLineEvent);
            LanClient.Instance.SquaresReceivedEvent += new Action<byte[]>(client_ClientSquaresReceivedEvent);
            LanClient.Instance.RemoteGameOverEvent+=new Action<int>(RemoteGameOverEvent);
            LanClient.Instance.MagicToolReceivedEvent+=new Action<MagicEnum>(Instance_MagicToolReceivedEvent);
            LanClient.Instance.RemoteScoreChangedEvent += new Action<int>(Instance_RemoteScoreChangedEvent);
            joinGame = new JoinLanGame();
            joinGame.ShowDialog();
        }

        void client_ServerOffLineEvent(IPEndPoint arg)
        {
            Player2OffLine(arg);
        }

        void client_ClientSquaresReceivedEvent(byte[] obj)
        {
            if (!GameEngine.IsLanGameStarted)
            {
                GameEngine.IsLanGameStarted = true;
                joinGame.DialogResult = DialogResult.OK;
                gameEngine.RestartGame();
                ShowLanPad(true);
            }
            InvalidatePlayer2View(obj);
        }

        private void player2View_Paint(object sender, PaintEventArgs e)
        {
            GameEngine.Instance.DrawFromDataGram(player2Bytes, e.Graphics);
        }

        private void nextPreviewContainer_Paint(object sender, PaintEventArgs e)
        {
            if (nextBlock != null)
                nextBlock.Draw(e.Graphics);
        }
    }
}
