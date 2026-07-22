namespace MinesweeperGUIApp
{
    partial class GameDifficultyForm
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
            gbDifficulty = new GroupBox();
            rdoHard = new RadioButton();
            rdoMedium = new RadioButton();
            rdoEasy = new RadioButton();
            rdoVeryEasy = new RadioButton();
            btnPlay = new Button();
            gbDifficulty.SuspendLayout();
            SuspendLayout();
            // 
            // gbDifficulty
            // 
            gbDifficulty.Controls.Add(rdoHard);
            gbDifficulty.Controls.Add(rdoMedium);
            gbDifficulty.Controls.Add(rdoEasy);
            gbDifficulty.Controls.Add(rdoVeryEasy);
            gbDifficulty.Location = new Point(12, 12);
            gbDifficulty.Name = "gbDifficulty";
            gbDifficulty.Size = new Size(131, 146);
            gbDifficulty.TabIndex = 0;
            gbDifficulty.TabStop = false;
            gbDifficulty.Text = "Choose a Difficulty";
            // 
            // rdoHard
            // 
            rdoHard.AutoSize = true;
            rdoHard.Location = new Point(18, 110);
            rdoHard.Name = "rdoHard";
            rdoHard.Size = new Size(51, 19);
            rdoHard.TabIndex = 3;
            rdoHard.TabStop = true;
            rdoHard.Text = "Hard";
            rdoHard.UseVisualStyleBackColor = true;
            rdoHard.Click += RdoDifficultyCheckChangedEH;
            // 
            // rdoMedium
            // 
            rdoMedium.AutoSize = true;
            rdoMedium.Location = new Point(18, 85);
            rdoMedium.Name = "rdoMedium";
            rdoMedium.Size = new Size(70, 19);
            rdoMedium.TabIndex = 2;
            rdoMedium.TabStop = true;
            rdoMedium.Text = "Medium";
            rdoMedium.UseVisualStyleBackColor = true;
            rdoMedium.Click += RdoDifficultyCheckChangedEH;
            // 
            // rdoEasy
            // 
            rdoEasy.AutoSize = true;
            rdoEasy.Location = new Point(18, 60);
            rdoEasy.Name = "rdoEasy";
            rdoEasy.Size = new Size(48, 19);
            rdoEasy.TabIndex = 1;
            rdoEasy.TabStop = true;
            rdoEasy.Text = "Easy";
            rdoEasy.UseVisualStyleBackColor = true;
            rdoEasy.Click += RdoDifficultyCheckChangedEH;
            // 
            // rdoVeryEasy
            // 
            rdoVeryEasy.AutoSize = true;
            rdoVeryEasy.Location = new Point(18, 35);
            rdoVeryEasy.Name = "rdoVeryEasy";
            rdoVeryEasy.Size = new Size(73, 19);
            rdoVeryEasy.TabIndex = 0;
            rdoVeryEasy.TabStop = true;
            rdoVeryEasy.Text = "Very Easy";
            rdoVeryEasy.UseVisualStyleBackColor = true;
            rdoVeryEasy.Click += RdoDifficultyCheckChangedEH;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(42, 164);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 23);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play!";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += BtnStartGameClickEH;
            // 
            // GameDifficultyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(159, 199);
            Controls.Add(btnPlay);
            Controls.Add(gbDifficulty);
            Name = "GameDifficultyForm";
            Text = "Form1";
            gbDifficulty.ResumeLayout(false);
            gbDifficulty.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbDifficulty;
        private RadioButton rdoEasy;
        private RadioButton rdoVeryEasy;
        private RadioButton rdoHard;
        private RadioButton rdoMedium;
        private Button btnPlay;
    }
}
