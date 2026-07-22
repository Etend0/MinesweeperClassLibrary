namespace MinesweeperGUIApp
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMinesweeperBoard = new Panel();
            label1 = new Label();
            lblTimer = new Label();
            lblScore = new Label();
            label3 = new Label();
            btnRestart = new Button();
            SuspendLayout();
            // 
            // pnlMinesweeperBoard
            // 
            pnlMinesweeperBoard.Location = new Point(12, 12);
            pnlMinesweeperBoard.Name = "pnlMinesweeperBoard";
            pnlMinesweeperBoard.Size = new Size(860, 706);
            pnlMinesweeperBoard.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(878, 40);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 1;
            label1.Text = "Start Time:";
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(878, 64);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(38, 15);
            lblTimer.TabIndex = 2;
            lblTimer.Text = "label2";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(878, 129);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(38, 15);
            lblScore.TabIndex = 4;
            lblScore.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(878, 105);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 3;
            label3.Text = "Score:";
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(920, 171);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(75, 23);
            btnRestart.TabIndex = 5;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += BtnResetGameEH;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1043, 883);
            Controls.Add(btnRestart);
            Controls.Add(lblScore);
            Controls.Add(label3);
            Controls.Add(lblTimer);
            Controls.Add(label1);
            Controls.Add(pnlMinesweeperBoard);
            Name = "GameForm";
            Text = "GameForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlMinesweeperBoard;
        private Label label1;
        private Label lblTimer;
        private Label lblScore;
        private Label label3;
        private Button btnRestart;
    }
}