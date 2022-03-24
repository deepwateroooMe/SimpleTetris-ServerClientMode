using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Timers;
using MySquare.Blocks;
using MySquare.Squares;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySquare.Utilities;
using MySquare.MagicTools;
using MySquare.LanGame;
using MySquare.DataGram;

namespace MySquare
{
    /// <summary>
    /// 游戏引擎
    /// </summary>
    public class GameEngine
    {
        public const int SQUARE_WIDTH = 25;//每个小方块的边长（以像素为单位）
        public const int FIELD_W = 12;//游戏区域的宽度（以小方块的个数为单位）
        public const int FIELD_H = 20;//游戏区域的高度（以小方块的个数为单位）
        private const int SPEED_UP_STEP = 2; //等级每增长一级，速度加快多少
        private const int LEVEL_UP_SCORE = 10;//满多少分，长一级
        private const int INIT_SPEED = 30;//初始速度

        private int speed = INIT_SPEED;//游戏速度（数值越小，速度越快）
        private Timer timerEngine = new Timer();//定时器
        private static GameEngine instance = null;//私有静态单例
        private int speedTimerTag;//计数标记
        private BaseBlock nextBlock = null;//下一个组合块
        private List<Square> metaSquares = new List<Square>();//游戏区域中的小方块的集合

        public int[,] matrix = null;//游戏矩阵，1代表对应位置有小方块，0代表没有小方块

        public static bool isRPG = true;

        public int SquareCount//小方块个数
        {
            get
            {
                return metaSquares.Count;
            }
        }

        public bool _IsSelfGameOver = false;//游戏是否结束
        public bool IsSelfGameOver
        {
            set
            {
                _IsSelfGameOver = value;
                timerEngine.Enabled = _IsSelfGameOver ? false : true;
                if (_IsSelfGameOver)
                {

                    SetDisableColor();
                }
            }
            get
            {
                return _IsSelfGameOver;
            }
        }

        public void SetDisableColor()
        {
            foreach (var s in metaSquares)
            {
                s.FillColor = Color.White;
            }
        }
        public int FieldIndexWidth//游戏区域的宽度（以像素为单位）
        {
            get
            {
                return SQUARE_WIDTH * FIELD_W;
            }
        }
        public int FieldIndexHeight//游戏区域的高度（以像素为单位）
        {
            get
            {
                return SQUARE_WIDTH * FIELD_H;
            }
        }
        public event Action<int> LevelUpEvent;//游戏等级增加事件
        public BaseBlock CurrentBlock { set; get; }//当前组合块
        
        private int _Score;
        public int Score //得分
        {
            set
            {
                _Score = value;

                DataGram<int> scoreGram = new DataGram<int>() { cmd = GramConst.SCORE_CHANGE, data = _Score };
                var buf = Serializer.Serialize(scoreGram);
                if (IsServer)
                {
                    LanServer.Instance.SendMsgToRemote(buf);
                }
                else
                {
                    LanClient.Instance.SendMsgToRemote(buf);
                }

                if (_Score % LEVEL_UP_SCORE == 0)
                {
                    if (LevelUpEvent != null)
                    {
                        LevelUpEvent(_Score / LEVEL_UP_SCORE);
                        ChangeSpeed(true);
                    }
                }
            }
            get
            {
                return _Score;
            }
        }

        public static GameEngine Instance//游戏引擎单例
        {
            get
            {
                if (instance == null)
                    instance = new GameEngine();
                return instance;
            }
        }
        public static bool IsLanGameStarted { set; get; }//局域网游戏是否开始
        public static bool IsServer { set; get; }//当前游戏是主机，还是客户机
        public event Action SelfGameOverEvent;//游戏结束事件
        public event Action GameStartEvent;//游戏开始事件
        public event Action InvalidateFieldEvent;//刷新游戏区域事件
        public event Action<BaseBlock> NewBlockEvent;//产生新组合块事件
        public event Action LineCompleteEvent;//某行满时的事件
        
        //当前方块集合中，最上面的方块Y坐标
        public int TopSquareY
        {
            get
            {
                var orderdeSquare = metaSquares.FindAll(s =>
                {
                    return !s.IsActive;
                }).OrderBy<Square, int>(s =>
                {
                    return s.IndexTop;
                });
                var list = orderdeSquare.ToList<Square>();
                if (list == null || list.Count == 0)
                    return FIELD_H;
                var first = list.First<Square>();
                return first.IndexTop;
            }
        }

        //把所有方块集合排序
        public IOrderedEnumerable<Square> OrderedMetaSquares
        {
            get
            {
                var orderdSquares = metaSquares.OrderBy<Square, int>(s =>
                {
                    return s.IndexTop;
                });
                return orderdSquares;
            }
        }

        //使用道具
        public void PerformMagic(MagicEnum m)
        {
            dicMagic[m].Work();
        }

        public event Action<int> MagicChangedEvent;
        public Dictionary<MagicEnum, MagicTool> dicMagic = new Dictionary<MagicEnum, MagicTool>();
        private int _MagicPower;
        //能量值
        public int MagicPower
        {
            set
            {
                _MagicPower = value;
                if (MagicChangedEvent != null)
                    MagicChangedEvent(_MagicPower);
            }
            get
            {
                return _MagicPower;
            }
        }
        #region 道具
        //移除第i行方块
        public void RemoveLine(int i)
        {
            if (IsSelfGameOver)
                return;
            if (LineCompleteEvent != null)
                LineCompleteEvent();
            //Matrix的行i全部设为0
            for (int a = 0; a < FIELD_W; a++)
            {
                matrix[i, a] = 0;
            }
            //去除行i的全部元素
            metaSquares = metaSquares.FindAll(s =>
                s.IndexTop != i
            );
 
            //行i上方的所有元素下移
            var aboveSquares = metaSquares.FindAll(s =>
            {
                //return (s.IndexTop < i);
                return (s.IndexTop < i && s.IsActive == false);
            });
            //按行号降序排序
            var orderedAbove = aboveSquares.OrderByDescending<Square, int>(s =>
            {
                return s.IndexTop;
            });
            foreach (var sq in orderedAbove)
            {
                //if (sq.IsActive)
                //    continue;
                sq.MoveDown();
                matrix[sq.IndexTop, sq.IndexLeft] = 1;
            }
        }
        //加速或减速
        public void ChangeSpeed(bool isFast)
        {
            var t = isFast ? speed - SPEED_UP_STEP : speed + SPEED_UP_STEP;
            if (t > 0)
                speed = t;
        }
        //往上顶一行方块
        public void Grow()
        {
            bool tag = true;
            for (int j = 0; j < FIELD_W; j++)
            {
                var list = this[j, 2];//如果方块已经顶到第二行，则不再继续往上顶
                if (list.Count > 0)
                {
                    var activeSquare = list.Find(s =>
                    {
                        return s.IsActive;
                    });
                    if (activeSquare == null)
                    {
                        tag = false;
                        break;
                    }
                }
            }
            if (!tag)
                return;

            //当前方块集合的高度到达活动方块时，不再往上顶
            if (CurrentBlock != null)
            {
                if (TopSquareY <= CurrentBlock.OutterBottom)
                    return;
            }

            //按照纵坐标排序，往上顶时，按照纵坐标从上往下向上移动
            foreach (var s in OrderedMetaSquares)
            {
                if (s.IsActive)
                    continue;
                s.MoveUp();
            }

            //在最下面一行，生成随机个数、随机位置的方块，从而实现往上顶的效果
            Random rand = new Random();
            int randNum = rand.Next(1, FIELD_W);
            List<int> listP = new List<int>();
            for (int i = 0; i < randNum; i++)
            {
                int randLeft = rand.Next(FIELD_W);
                if (listP.Contains(randLeft))
                    continue;
                listP.Add(randLeft);
                Square sq = new Square();
                sq.PixelLeft = randLeft * SQUARE_WIDTH;
                sq.PixelTop = (FIELD_H - 1) * SQUARE_WIDTH;
                sq.FillColor = Color.White;
                metaSquares.Add(sq);
                matrix[sq.IndexTop, sq.IndexLeft] = 1;
            }
        } 
        #endregion

        //索引器，返回指定位置处的所有方块的集合
        public List<Square> this[int x, int y]
        {
            get
            {
                List<Square> r = new List<Square>();
                for (int i = metaSquares.Count - 1; i >= 0; i--)
                {
                    if (metaSquares[i].IndexLeft == x && metaSquares[i].IndexTop == y)
                        r.Add(metaSquares[i]);
                }
                return r;
            }
        }

        public void DrawAllSquares(Graphics g,List<Square> squares)
        {
            var orderedSquares = squares.OrderBy<Square, int>(s => s.IndexTop);
            foreach (var s in orderedSquares)
            {
                s.Draw(g);
            }
        }

        public void DrawAllSquares(Graphics g)
        {
            DrawAllSquares(g, metaSquares);
        }
        public void DrawTipImg(Graphics g)
        {
            g.DrawImage(Image.FromFile(@"C:\Users\Public\Pictures\Sample Pictures\f.jpg"), Point.Empty);
        }
        //把所有方块集合转化为字节序列
        public byte[] SquareBytes
        {
            get
            {
                List<SquareData> r = new List<SquareData>();
                foreach (var s in OrderedMetaSquares)
                {
                    r.Add(new SquareData() { canSplash = s.CanSplash, fillColor = s.FillColor, pixelLeft = s.PixelLeft, pixelTop = s.PixelTop, visible = s.Visible });
                }
                DataGram<List<SquareData>> data = new DataGram<List<SquareData>>() { cmd = GramConst.SQUARE_DATA, data = r };
                byte[] buf = Serializer.Serialize(data);
                return buf;
            }
        }
        public void DrawFromDataGram(byte[] buf, Graphics g)
        {
            if (buf == null)
                return;
            var receiveData = Serializer.Deserialize<DataGram<List<SquareData>>>(buf);
            if (receiveData != null && receiveData.cmd == GramConst.SQUARE_DATA)
                DrawFromDataGram(receiveData.data, g);
        }
        public void DrawFromDataGram(List<SquareData> grams,Graphics g)
        {
            List<Square> squares = new List<Square>();
            foreach (var gram in grams)
            {
                Square square = null;
                if (gram.canSplash)
                {
                    square = new SuperSquare() { CanSplash = gram.canSplash, FillColor = gram.fillColor, PixelLeft = gram.pixelLeft, PixelTop = gram.pixelTop, Visible = gram.visible };
                }
                else
                {
                    square = new Square() { CanSplash = gram.canSplash, FillColor = gram.fillColor, PixelLeft = gram.pixelLeft, PixelTop = gram.pixelTop, Visible = gram.visible };
                }
                
                squares.Add(square);
            }
            DrawAllSquares(g, squares);
        }
        private GameEngine()
        {
            timerEngine.Interval = 10;
            timerEngine.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            matrix = new int[FIELD_H, FIELD_W];

            LoadMagics();
            
        }
        private void LoadMagics()
        {
            dicMagic.Add(MagicEnum.REMOVE_LAST_LINE, new RemoveLastLineMagic());
            dicMagic.Add(MagicEnum.GROW, new GrowMagic());
            dicMagic.Add(MagicEnum.SPEED_UP, new SpeedUpMagic());
            dicMagic.Add(MagicEnum.SPEED_DOWN, new SpeedDownMagic());
            dicMagic.Add(MagicEnum.RANDOM_BLOCK, new RandomBlockMagic());
        }
        private bool isPause = false;
        private int pauseTag = 0;
        private int pauseInterval = 100;
        public void Pause()
        {
            isPause = true;
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //暂停道具
            if (isPause)
            {
                if (pauseTag >= int.MaxValue)
                    pauseTag = 0;
                if (pauseTag >= pauseInterval)
                {
                    isPause = false;
                    pauseTag = 0;
                }
                pauseTag++;
                return;
            }

            if (speedTimerTag >= int.MaxValue)
                speedTimerTag = 0;
            if (speedTimerTag % speed == 0)
            {
                if (CurrentBlock != null)
                    CurrentBlock.MoveDown();
            }
            //else
            {
                if (InvalidateFieldEvent != null)
                    InvalidateFieldEvent();
            }
            speedTimerTag++;
        }

        private void AddSquares(BaseBlock block)
        {
            metaSquares.AddRange(block.Squares);
        }

        public string PauseGame()
        {
            if (!IsSelfGameOver)
            {
                timerEngine.Enabled = !timerEngine.Enabled;
                return timerEngine.Enabled == true ? "游戏运行" : "游戏暂停";
            }
            return "游戏结束";
        }
        public void StopListen()
        {
            if (GameEngine.IsServer)
            {
                LanServer.Instance.StopListen();
            }
            else
            {
                LanClient.Instance.StopListen();
            }
        }
        private void NewBlock()
        {
            if (nextBlock != null)
            {
                nextBlock.ArriveBottomEvent += new Action(block_ArriveBottomEvent);
                CurrentBlock = nextBlock;
                AddSquares(nextBlock);
                if (!nextBlock.CanDown())
                {
                    //GameOver
                    IsSelfGameOver = true;
                    if (SelfGameOverEvent != null)
                    {
                        MagicPower = 0;
                        DataGram<int> gameOverGram = new DataGram<int>() { cmd = GramConst.GAME_OVER, data = Score };
                        byte[] buf = Serializer.Serialize(gameOverGram);
                        if (!isRPG)
                        {
                            if (GameEngine.IsServer)
                            {
                                LanServer.Instance.SendMsgToRemote(buf);
                            }
                            else
                            {
                                LanClient.Instance.SendMsgToRemote(buf);
                            }
                        }
                        StopListen();
                        SelfGameOverEvent();
                    }
                    return;
                }
            }
            var next = BlockFactory.Create();
            PrepareNextBlock(next);
        }

        public void PrepareNextBlock(BaseBlock next)
        {
            nextBlock = next;
            if (NewBlockEvent != null)
                NewBlockEvent(nextBlock);
        }

        void block_ArriveBottomEvent()
        {
            if (IsSelfGameOver)
                return;
            CurrentBlock.IsActive = false;
            NewBlock();
            CheckLine();
        }
        public void Reset()
        {
            LanServer.Instance.StopListen();
            LanServer.Instance.StopBroadcast();
            LanServer.Instance.StopRepeatSend();

            LanClient.Instance.StopListen();
            LanClient.Instance.StopRepeatSend();
            LanClient.lanGameJoined = true;
        }
        public void RestartGame()
        {
            speed = INIT_SPEED;
            Score = 0;
            MagicPower = 0;
            IsSelfGameOver = false;
            if (metaSquares != null)
                metaSquares.Clear();
            ClearMatrix();
            
            nextBlock = BlockFactory.Create();
            if (NewBlockEvent != null)
                NewBlockEvent(nextBlock);

            NewBlock();
            if (GameStartEvent != null)
                GameStartEvent();
        }
        private void ClearMatrix()
        {
            for (int i = 0; i < FIELD_H; i++)
                for (int j = 0; j < FIELD_W; j++)
                    matrix[i, j] = 0;
        }
        public void RemoveLastLine()
        {
            RemoveLine(FIELD_H - 1);
        }
        public void CheckLine()
        {
            int fullRowsCount = 0;
            for (int i = 0; i < FIELD_H; i++)//逐行检测是否有满行
            {
                bool tag = true;
                for (int j = 0; j < FIELD_W; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        tag = false;
                        break;
                    }
                }
                if (tag)//如果满行
                {
                    Score++;
                    fullRowsCount++;
                    RemoveLine(i);
                }
            }

            //道具能量条增加策略
            int alpha = 2;//一次性填满alpha行，才能获得能量增加
            if (fullRowsCount >= alpha)
            {
                MagicPower += ((fullRowsCount - alpha) * 5 + 10);
            }
        }
    }
}
