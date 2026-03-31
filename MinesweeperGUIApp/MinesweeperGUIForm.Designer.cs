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
            SuspendLayout();
            // 
            // pnlMinesweeperBoard
            // 
            pnlMinesweeperBoard.Location = new Point(12, 22);
            pnlMinesweeperBoard.Name = "pnlMinesweeperBoard";
            pnlMinesweeperBoard.Size = new Size(753, 740);
            pnlMinesweeperBoard.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(780, 63);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 1;
            label1.Text = "Start Time";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(780, 87);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(64, 15);
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
            btnScores.Location = new Point(815, 616);
            btnScores.Name = "btnScores";
            btnScores.Size = new Size(75, 23);
            btnScores.TabIndex = 6;
            btnScores.Text = "See Scores";
            btnScores.UseVisualStyleBackColor = true;
            btnScores.Click += BtnHighscoresEH;
            // 
            // MinesweeperGUIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 774);
            Controls.Add(btnScores);
            Controls.Add(BtnRestart);
            Controls.Add(lblScore);
            Controls.Add(label3);
            Controls.Add(lblStartTime);
            Controls.Add(label1);
            Controls.Add(pnlMinesweeperBoard);
            Name = "MinesweeperGUIForm";
            Text = "Minesweeper";
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
    }
}
