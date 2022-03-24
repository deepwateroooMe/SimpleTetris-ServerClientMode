namespace MySquare
{
    partial class MainPad
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPad));
            this.mainContainer = new System.Windows.Forms.PictureBox();
            this.nextPreviewContainer = new System.Windows.Forms.PictureBox();
            this.labelNext = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.magicRandomBlock = new System.Windows.Forms.Label();
            this.magicSpeedDown = new System.Windows.Forms.Label();
            this.magicGrow = new System.Windows.Forms.Label();
            this.magicSpeedUp = new System.Windows.Forms.Label();
            this.magicRemoveLastLine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.scoreCanvas = new System.Windows.Forms.PictureBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelCurrentLevel = new System.Windows.Forms.Label();
            this.labelCurrentScore = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始新游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.暂停继续ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.联网游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.建立主机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加入游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gameStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.HostOrClientStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.player2View = new System.Windows.Forms.PictureBox();
            this.labelVs = new System.Windows.Forms.Label();
            this.player2ScoreCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextPreviewContainer)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreCanvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2ScoreCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.BackColor = System.Drawing.Color.Black;
            this.mainContainer.Location = new System.Drawing.Point(12, 28);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(300, 500);
            this.mainContainer.TabIndex = 0;
            this.mainContainer.TabStop = false;
            this.mainContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.mainContainer_Paint);
            // 
            // nextPreviewContainer
            // 
            this.nextPreviewContainer.BackColor = System.Drawing.Color.Black;
            this.nextPreviewContainer.Location = new System.Drawing.Point(9, 8);
            this.nextPreviewContainer.Name = "nextPreviewContainer";
            this.nextPreviewContainer.Size = new System.Drawing.Size(100, 100);
            this.nextPreviewContainer.TabIndex = 1;
            this.nextPreviewContainer.TabStop = false;
            this.nextPreviewContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.nextPreviewContainer_Paint);
            // 
            // labelNext
            // 
            this.labelNext.AutoSize = true;
            this.labelNext.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNext.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelNext.Location = new System.Drawing.Point(30, 113);
            this.labelNext.Name = "labelNext";
            this.labelNext.Size = new System.Drawing.Size(58, 22);
            this.labelNext.TabIndex = 2;
            this.labelNext.Text = "下一个";
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panelRight.Controls.Add(this.panel1);
            this.panelRight.Controls.Add(this.progressBar1);
            this.panelRight.Controls.Add(this.scoreCanvas);
            this.panelRight.Controls.Add(this.labelLevel);
            this.panelRight.Controls.Add(this.labelCurrentLevel);
            this.panelRight.Controls.Add(this.nextPreviewContainer);
            this.panelRight.Controls.Add(this.labelCurrentScore);
            this.panelRight.Controls.Add(this.labelNext);
            this.panelRight.Location = new System.Drawing.Point(318, 29);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(118, 500);
            this.panelRight.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.magicRandomBlock);
            this.panel1.Controls.Add(this.magicSpeedDown);
            this.panel1.Controls.Add(this.magicGrow);
            this.panel1.Controls.Add(this.magicSpeedUp);
            this.panel1.Controls.Add(this.magicRemoveLastLine);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 281);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 190);
            this.panel1.TabIndex = 8;
            // 
            // magicRandomBlock
            // 
            this.magicRandomBlock.AutoSize = true;
            this.magicRandomBlock.BackColor = System.Drawing.Color.Black;
            this.magicRandomBlock.Font = new System.Drawing.Font("宋体", 12F);
            this.magicRandomBlock.ForeColor = System.Drawing.Color.Lime;
            this.magicRandomBlock.Location = new System.Drawing.Point(22, 164);
            this.magicRandomBlock.Name = "magicRandomBlock";
            this.magicRandomBlock.Size = new System.Drawing.Size(56, 16);
            this.magicRandomBlock.TabIndex = 0;
            this.magicRandomBlock.Text = "5:随机";
            // 
            // magicSpeedDown
            // 
            this.magicSpeedDown.AutoSize = true;
            this.magicSpeedDown.BackColor = System.Drawing.Color.Black;
            this.magicSpeedDown.Font = new System.Drawing.Font("宋体", 12F);
            this.magicSpeedDown.ForeColor = System.Drawing.Color.Lime;
            this.magicSpeedDown.Location = new System.Drawing.Point(22, 133);
            this.magicSpeedDown.Name = "magicSpeedDown";
            this.magicSpeedDown.Size = new System.Drawing.Size(56, 16);
            this.magicSpeedDown.TabIndex = 0;
            this.magicSpeedDown.Text = "4:减速";
            // 
            // magicGrow
            // 
            this.magicGrow.AutoSize = true;
            this.magicGrow.BackColor = System.Drawing.Color.Black;
            this.magicGrow.Font = new System.Drawing.Font("宋体", 12F);
            this.magicGrow.ForeColor = System.Drawing.Color.LightCoral;
            this.magicGrow.Location = new System.Drawing.Point(22, 71);
            this.magicGrow.Name = "magicGrow";
            this.magicGrow.Size = new System.Drawing.Size(56, 16);
            this.magicGrow.TabIndex = 0;
            this.magicGrow.Text = "2:增长";
            // 
            // magicSpeedUp
            // 
            this.magicSpeedUp.AutoSize = true;
            this.magicSpeedUp.BackColor = System.Drawing.Color.Black;
            this.magicSpeedUp.Font = new System.Drawing.Font("宋体", 12F);
            this.magicSpeedUp.ForeColor = System.Drawing.Color.LightCoral;
            this.magicSpeedUp.Location = new System.Drawing.Point(22, 102);
            this.magicSpeedUp.Name = "magicSpeedUp";
            this.magicSpeedUp.Size = new System.Drawing.Size(56, 16);
            this.magicSpeedUp.TabIndex = 0;
            this.magicSpeedUp.Text = "3:加速";
            // 
            // magicRemoveLastLine
            // 
            this.magicRemoveLastLine.AutoSize = true;
            this.magicRemoveLastLine.BackColor = System.Drawing.Color.Black;
            this.magicRemoveLastLine.Font = new System.Drawing.Font("宋体", 12F);
            this.magicRemoveLastLine.ForeColor = System.Drawing.Color.Lime;
            this.magicRemoveLastLine.Location = new System.Drawing.Point(22, 40);
            this.magicRemoveLastLine.Name = "magicRemoveLastLine";
            this.magicRemoveLastLine.Size = new System.Drawing.Size(56, 16);
            this.magicRemoveLastLine.TabIndex = 0;
            this.magicRemoveLastLine.Text = "1:消行";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "道具";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 477);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(118, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // scoreCanvas
            // 
            this.scoreCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.scoreCanvas.Location = new System.Drawing.Point(9, 138);
            this.scoreCanvas.Name = "scoreCanvas";
            this.scoreCanvas.Size = new System.Drawing.Size(100, 50);
            this.scoreCanvas.TabIndex = 4;
            this.scoreCanvas.TabStop = false;
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.BackColor = System.Drawing.Color.Transparent;
            this.labelLevel.Font = new System.Drawing.Font("宋体", 24F);
            this.labelLevel.ForeColor = System.Drawing.Color.Black;
            this.labelLevel.Location = new System.Drawing.Point(44, 220);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(31, 33);
            this.labelLevel.TabIndex = 3;
            this.labelLevel.Text = "0";
            // 
            // labelCurrentLevel
            // 
            this.labelCurrentLevel.AutoSize = true;
            this.labelCurrentLevel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentLevel.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCurrentLevel.Location = new System.Drawing.Point(38, 256);
            this.labelCurrentLevel.Name = "labelCurrentLevel";
            this.labelCurrentLevel.Size = new System.Drawing.Size(42, 22);
            this.labelCurrentLevel.TabIndex = 2;
            this.labelCurrentLevel.Text = "等级";
            // 
            // labelCurrentScore
            // 
            this.labelCurrentScore.AutoSize = true;
            this.labelCurrentScore.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentScore.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labelCurrentScore.Location = new System.Drawing.Point(22, 193);
            this.labelCurrentScore.Name = "labelCurrentScore";
            this.labelCurrentScore.Size = new System.Drawing.Size(74, 22);
            this.labelCurrentScore.TabIndex = 2;
            this.labelCurrentScore.Text = "当前得分";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.游戏ToolStripMenuItem,
            this.联网游戏ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(793, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 游戏ToolStripMenuItem
            // 
            this.游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始新游戏ToolStripMenuItem,
            this.toolStripSeparator1,
            this.暂停继续ToolStripMenuItem});
            this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
            this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.游戏ToolStripMenuItem.Text = "单机游戏";
            // 
            // 开始新游戏ToolStripMenuItem
            // 
            this.开始新游戏ToolStripMenuItem.Name = "开始新游戏ToolStripMenuItem";
            this.开始新游戏ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.开始新游戏ToolStripMenuItem.Text = "新游戏(&S)";
            this.开始新游戏ToolStripMenuItem.Click += new System.EventHandler(this.开始新游戏ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // 暂停继续ToolStripMenuItem
            // 
            this.暂停继续ToolStripMenuItem.Name = "暂停继续ToolStripMenuItem";
            this.暂停继续ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.暂停继续ToolStripMenuItem.Text = "暂停/继续(&P)";
            this.暂停继续ToolStripMenuItem.Click += new System.EventHandler(this.暂停继续ToolStripMenuItem_Click);
            // 
            // 联网游戏ToolStripMenuItem
            // 
            this.联网游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.建立主机ToolStripMenuItem,
            this.加入游戏ToolStripMenuItem});
            this.联网游戏ToolStripMenuItem.Name = "联网游戏ToolStripMenuItem";
            this.联网游戏ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.联网游戏ToolStripMenuItem.Text = "联网游戏";
            // 
            // 建立主机ToolStripMenuItem
            // 
            this.建立主机ToolStripMenuItem.Name = "建立主机ToolStripMenuItem";
            this.建立主机ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.建立主机ToolStripMenuItem.Text = "建立游戏";
            this.建立主机ToolStripMenuItem.Click += new System.EventHandler(this.建立主机ToolStripMenuItem_Click);
            // 
            // 加入游戏ToolStripMenuItem
            // 
            this.加入游戏ToolStripMenuItem.Name = "加入游戏ToolStripMenuItem";
            this.加入游戏ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.加入游戏ToolStripMenuItem.Text = "加入游戏";
            this.加入游戏ToolStripMenuItem.Click += new System.EventHandler(this.加入游戏ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameStatus,
            this.HostOrClientStatus,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(793, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gameStatus
            // 
            this.gameStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.gameStatus.Name = "gameStatus";
            this.gameStatus.Size = new System.Drawing.Size(56, 17);
            this.gameStatus.Text = "游戏状态";
            // 
            // HostOrClientStatus
            // 
            this.HostOrClientStatus.Name = "HostOrClientStatus";
            this.HostOrClientStatus.Size = new System.Drawing.Size(32, 17);
            this.HostOrClientStatus.Text = "主机";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Gray;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 17);
            this.toolStripStatusLabel1.Text = "Designed by rufeng2000";
            // 
            // player2View
            // 
            this.player2View.BackColor = System.Drawing.Color.Gray;
            this.player2View.Location = new System.Drawing.Point(488, 29);
            this.player2View.Name = "player2View";
            this.player2View.Size = new System.Drawing.Size(300, 500);
            this.player2View.TabIndex = 6;
            this.player2View.TabStop = false;
            this.player2View.Paint += new System.Windows.Forms.PaintEventHandler(this.player2View_Paint);
            // 
            // labelVs
            // 
            this.labelVs.AutoSize = true;
            this.labelVs.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.labelVs.ForeColor = System.Drawing.Color.Red;
            this.labelVs.Location = new System.Drawing.Point(440, 237);
            this.labelVs.Name = "labelVs";
            this.labelVs.Size = new System.Drawing.Size(45, 31);
            this.labelVs.TabIndex = 2;
            this.labelVs.Text = "VS";
            // 
            // player2ScoreCanvas
            // 
            this.player2ScoreCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.player2ScoreCanvas.Location = new System.Drawing.Point(681, 37);
            this.player2ScoreCanvas.Name = "player2ScoreCanvas";
            this.player2ScoreCanvas.Size = new System.Drawing.Size(100, 50);
            this.player2ScoreCanvas.TabIndex = 4;
            this.player2ScoreCanvas.TabStop = false;
            // 
            // MainPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(793, 554);
            this.Controls.Add(this.player2ScoreCanvas);
            this.Controls.Add(this.player2View);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelVs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "rufeng2000Tetris";
            this.Deactivate += new System.EventHandler(this.MainPad_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextPreviewContainer)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreCanvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2ScoreCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainContainer;
        private System.Windows.Forms.PictureBox nextPreviewContainer;
        private System.Windows.Forms.Label labelNext;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始新游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 暂停继续ToolStripMenuItem;
        private System.Windows.Forms.Label labelCurrentScore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel gameStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelCurrentLevel;
        private System.Windows.Forms.PictureBox scoreCanvas;
        private System.Windows.Forms.ToolStripMenuItem 联网游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 建立主机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加入游戏ToolStripMenuItem;
        private System.Windows.Forms.PictureBox player2View;
        private System.Windows.Forms.Label labelVs;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label magicGrow;
        private System.Windows.Forms.Label magicRemoveLastLine;
        private System.Windows.Forms.Label magicSpeedDown;
        private System.Windows.Forms.Label magicSpeedUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel HostOrClientStatus;
        private System.Windows.Forms.PictureBox player2ScoreCanvas;
        private System.Windows.Forms.Label magicRandomBlock;

    }
}

