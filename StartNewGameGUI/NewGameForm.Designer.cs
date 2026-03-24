namespace StartNewGameGUI
{
    partial class NewGameForm
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
            gbNewLevel = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            btnPlay = new Button();
            gbNewLevel.SuspendLayout();
            SuspendLayout();
            // 
            // gbNewLevel
            // 
            gbNewLevel.Controls.Add(radioButton3);
            gbNewLevel.Controls.Add(radioButton4);
            gbNewLevel.Controls.Add(radioButton2);
            gbNewLevel.Controls.Add(radioButton1);
            gbNewLevel.Location = new Point(12, 12);
            gbNewLevel.Name = "gbNewLevel";
            gbNewLevel.Size = new Size(135, 139);
            gbNewLevel.TabIndex = 0;
            gbNewLevel.TabStop = false;
            gbNewLevel.Text = "Choose a Level";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(19, 105);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(51, 19);
            radioButton3.TabIndex = 3;
            radioButton3.TabStop = true;
            radioButton3.Text = "Hard";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.Click += RdoDifficultyCheckChangedEH;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(19, 80);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(70, 19);
            radioButton4.TabIndex = 2;
            radioButton4.TabStop = true;
            radioButton4.Text = "Medium";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.Click += RdoDifficultyCheckChangedEH;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(19, 56);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(48, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Easy";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.Click += RdoDifficultyCheckChangedEH;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(19, 31);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(73, 19);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Very Easy";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Click += RdoDifficultyCheckChangedEH;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(43, 160);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 23);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play!";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += BtnStartGameClickEH;
            // 
            // NewGameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(162, 195);
            Controls.Add(btnPlay);
            Controls.Add(gbNewLevel);
            Name = "NewGameForm";
            Text = "Start a New Game";
            gbNewLevel.ResumeLayout(false);
            gbNewLevel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbNewLevel;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button btnPlay;
    }
}
