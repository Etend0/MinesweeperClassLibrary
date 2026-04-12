namespace FrmHighscores
{
    partial class FrmHighscores
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
            dtgdHighscores = new DataGridView();
            mnuOptions = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            sortToolStripMenuItem = new ToolStripMenuItem();
            byNameToolStripMenuItem = new ToolStripMenuItem();
            byScoreToolStripMenuItem = new ToolStripMenuItem();
            byDateToolStripMenuItem = new ToolStripMenuItem();
            lblPlayerName = new Label();
            lblAverageTime = new Label();
            label1 = new Label();
            label2 = new Label();
            lblAverageScore = new Label();
            pltScores = new ScottPlot.WinForms.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)dtgdHighscores).BeginInit();
            mnuOptions.SuspendLayout();
            SuspendLayout();
            // 
            // dtgdHighscores
            // 
            dtgdHighscores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgdHighscores.Location = new Point(12, 35);
            dtgdHighscores.Name = "dtgdHighscores";
            dtgdHighscores.Size = new Size(561, 403);
            dtgdHighscores.TabIndex = 0;
            dtgdHighscores.CellClick += DtgdHighscores_SelectionChanged;
            // 
            // mnuOptions
            // 
            mnuOptions.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, sortToolStripMenuItem });
            mnuOptions.Location = new Point(0, 0);
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new Size(996, 24);
            mnuOptions.TabIndex = 1;
            mnuOptions.Text = "mnuOptions";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, exitToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(37, 20);
            toolStripMenuItem1.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(100, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(100, 22);
            loadToolStripMenuItem.Text = "Load";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(100, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // sortToolStripMenuItem
            // 
            sortToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { byNameToolStripMenuItem, byScoreToolStripMenuItem, byDateToolStripMenuItem });
            sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            sortToolStripMenuItem.Size = new Size(40, 20);
            sortToolStripMenuItem.Text = "Sort";
            // 
            // byNameToolStripMenuItem
            // 
            byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            byNameToolStripMenuItem.Size = new Size(122, 22);
            byNameToolStripMenuItem.Text = "By Name";
            // 
            // byScoreToolStripMenuItem
            // 
            byScoreToolStripMenuItem.Name = "byScoreToolStripMenuItem";
            byScoreToolStripMenuItem.Size = new Size(122, 22);
            byScoreToolStripMenuItem.Text = "By Score";
            // 
            // byDateToolStripMenuItem
            // 
            byDateToolStripMenuItem.Name = "byDateToolStripMenuItem";
            byDateToolStripMenuItem.Size = new Size(122, 22);
            byDateToolStripMenuItem.Text = "By Date";
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Location = new Point(34, 477);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(38, 15);
            lblPlayerName.TabIndex = 2;
            lblPlayerName.Text = "label1";
            // 
            // lblAverageTime
            // 
            lblAverageTime.AutoSize = true;
            lblAverageTime.Location = new Point(167, 477);
            lblAverageTime.Name = "lblAverageTime";
            lblAverageTime.Size = new Size(38, 15);
            lblAverageTime.TabIndex = 3;
            lblAverageTime.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(78, 477);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 4;
            label1.Text = "Average Time:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(220, 477);
            label2.Name = "label2";
            label2.Size = new Size(85, 15);
            label2.TabIndex = 6;
            label2.Text = "Average Score:";
            // 
            // lblAverageScore
            // 
            lblAverageScore.AutoSize = true;
            lblAverageScore.Location = new Point(309, 477);
            lblAverageScore.Name = "lblAverageScore";
            lblAverageScore.Size = new Size(38, 15);
            lblAverageScore.TabIndex = 5;
            lblAverageScore.Text = "label1";
            // 
            // pltScores
            // 
            pltScores.Location = new Point(579, 35);
            pltScores.Name = "pltScores";
            pltScores.Size = new Size(405, 403);
            pltScores.TabIndex = 7;
            // 
            // FrmHighscores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(996, 528);
            Controls.Add(pltScores);
            Controls.Add(label2);
            Controls.Add(lblAverageScore);
            Controls.Add(label1);
            Controls.Add(lblAverageTime);
            Controls.Add(lblPlayerName);
            Controls.Add(dtgdHighscores);
            Controls.Add(mnuOptions);
            MainMenuStrip = mnuOptions;
            Name = "FrmHighscores";
            Text = "Highscores";
            ((System.ComponentModel.ISupportInitialize)dtgdHighscores).EndInit();
            mnuOptions.ResumeLayout(false);
            mnuOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtgdHighscores;
        private MenuStrip mnuOptions;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem sortToolStripMenuItem;
        private ToolStripMenuItem byNameToolStripMenuItem;
        private ToolStripMenuItem byScoreToolStripMenuItem;
        private ToolStripMenuItem byDateToolStripMenuItem;
        private Label lblPlayerName;
        private Label lblAverageTime;
        private Label label1;
        private Label label2;
        private Label lblAverageScore;
        private ScottPlot.WinForms.FormsPlot pltScores;
    }
}
