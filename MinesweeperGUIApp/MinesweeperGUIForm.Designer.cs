namespace MinesweeperGUIApp
{
    partial class MinesweeperGUIForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMinesweeperBoard = new Panel();
            label1 = new Label();
            lblStartTime = new Label();
            lblScore = new Label();
            label3 = new Label();
            BtnRestart = new Button();
            btnScores = new Button();
            label2 = new Label();
            btnUseReward = new Button();
            lblRewards = new Label();
            mnOptions = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            saveGameToolStripMenuItem = new ToolStripMenuItem();
            loadGameToolStripMenuItem = new ToolStripMenuItem();
            graphicsToolStripMenuItem = new ToolStripMenuItem();
            defaultToolStripMenuItem = new ToolStripMenuItem();
            classicToolStripMenuItem = new ToolStripMenuItem();
            mnOptions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMinesweeperBoard
            // 
            pnlMinesweeperBoard.Location = new Point(12, 37);
            pnlMinesweeperBoard.Name = "pnlMinesweeperBoard";
            pnlMinesweeperBoard.Size = new Size(753, 740);
            pnlMinesweeperBoard.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(780, 63);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 1;
            label1.Text = "Start Time";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(780, 87);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(65, 15);
            lblStartTime.TabIndex = 2;
            lblStartTime.Text = "GameTime";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(780, 161);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(67, 15);
            lblScore.TabIndex = 4;
            lblScore.Text = "GameScore";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(780, 137);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 3;
            label3.Text = "Score";
            // 
            // BtnRestart
            // 
            BtnRestart.Location = new Point(815, 210);
            BtnRestart.Name = "BtnRestart";
            BtnRestart.Size = new Size(75, 23);
            BtnRestart.TabIndex = 5;
            BtnRestart.Text = "Restart";
            BtnRestart.UseVisualStyleBackColor = true;
            BtnRestart.Click += BtnResetGameEH;
            // 
            // btnScores
            // 
            btnScores.Location = new Point(815, 721);
            btnScores.Name = "btnScores";
            btnScores.Size = new Size(75, 23);
            btnScores.TabIndex = 6;
            btnScores.Text = "See Scores";
            btnScores.UseVisualStyleBackColor = true;
            btnScores.Click += BtnHighscoresEH;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(780, 423);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 7;
            label2.Text = "Rewards";
            // 
            // btnUseReward
            // 
            btnUseReward.Location = new Point(801, 484);
            btnUseReward.Name = "btnUseReward";
            btnUseReward.Size = new Size(109, 46);
            btnUseReward.TabIndex = 8;
            btnUseReward.Text = "Use Reward";
            btnUseReward.UseVisualStyleBackColor = true;
            btnUseReward.Click += btnUseRewardEH;
            // 
            // lblRewards
            // 
            lblRewards.AutoSize = true;
            lblRewards.Location = new Point(780, 451);
            lblRewards.Name = "lblRewards";
            lblRewards.Size = new Size(38, 15);
            lblRewards.TabIndex = 9;
            lblRewards.Text = "label4";
            // 
            // mnOptions
            // 
            mnOptions.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, graphicsToolStripMenuItem });
            mnOptions.Location = new Point(0, 0);
            mnOptions.Name = "mnOptions";
            mnOptions.Size = new Size(942, 24);
            mnOptions.TabIndex = 10;
            mnOptions.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { saveGameToolStripMenuItem, loadGameToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(37, 20);
            toolStripMenuItem1.Text = "File";
            // 
            // saveGameToolStripMenuItem
            // 
            saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            saveGameToolStripMenuItem.Size = new Size(100, 22);
            saveGameToolStripMenuItem.Text = "Save";
            // 
            // loadGameToolStripMenuItem
            // 
            loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            loadGameToolStripMenuItem.Size = new Size(100, 22);
            loadGameToolStripMenuItem.Text = "Load";
            // 
            // graphicsToolStripMenuItem
            // 
            graphicsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { defaultToolStripMenuItem, classicToolStripMenuItem });
            graphicsToolStripMenuItem.Name = "graphicsToolStripMenuItem";
            graphicsToolStripMenuItem.Size = new Size(65, 20);
            graphicsToolStripMenuItem.Text = "Graphics";
            // 
            // defaultToolStripMenuItem
            // 
            defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            defaultToolStripMenuItem.Size = new Size(180, 22);
            defaultToolStripMenuItem.Text = "Default";
            defaultToolStripMenuItem.Click += defaultToolStripMenuItem_Click;
            // 
            // classicToolStripMenuItem
            // 
            classicToolStripMenuItem.Name = "classicToolStripMenuItem";
            classicToolStripMenuItem.Size = new Size(180, 22);
            classicToolStripMenuItem.Text = "Classic";
            classicToolStripMenuItem.Click += classicToolStripMenuItem_Click;
            // 
            // MinesweeperGUIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 789);
            Controls.Add(lblRewards);
            Controls.Add(btnUseReward);
            Controls.Add(label2);
            Controls.Add(btnScores);
            Controls.Add(BtnRestart);
            Controls.Add(lblScore);
            Controls.Add(label3);
            Controls.Add(lblStartTime);
            Controls.Add(label1);
            Controls.Add(pnlMinesweeperBoard);
            Controls.Add(mnOptions);
            MainMenuStrip = mnOptions;
            Name = "MinesweeperGUIForm";
            Text = "Minesweeper";
            mnOptions.ResumeLayout(false);
            mnOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlMinesweeperBoard;
        private Label label1;
        private Label lblStartTime;
        private Label lblScore;
        private Label label3;
        private Button BtnRestart;
        private Button btnScores;
        private Label label2;
        private Button btnUseReward;
        private Label lblRewards;
        private MenuStrip mnOptions;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private ToolStripMenuItem graphicsToolStripMenuItem;
        private ToolStripMenuItem defaultToolStripMenuItem;
        private ToolStripMenuItem classicToolStripMenuItem;
    }
}
