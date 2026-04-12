using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.MinesweeperDAO;
using static System.Windows.Forms.LinkLabel;
using ScottPlot;

namespace FrmHighscores
{
    public partial class FrmHighscores : Form
    {
        // Create an instance of the MinesweeperDAO
        private MinesweeperDAO _minesweeperDAO = new MinesweeperDAO();

        // Class level variables
        private List<GameState> _playerScores;

        /// <summary>
        /// Public constructor for FrmHighscores
        /// </summary>
        public FrmHighscores()
        {
            InitializeComponent();

            // Initialize the dropdown menu event handlers
            loadToolStripMenuItem.Click += LoadToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            byNameToolStripMenuItem.Click += ByNameToolStripMenuItem_Click;
            byScoreToolStripMenuItem.Click += ByScoreToolStripMenuItem_Click;
            byDateToolStripMenuItem.Click += ByDateToolStripMenuItem_Click;

            // Add event handler for DataGridView selection changed
            dtgdHighscores.SelectionChanged += DtgdHighscores_SelectionChanged;

            lblPlayerName.Text = string.Empty;
            lblAverageTime.Text = "00:00:00";
            lblAverageScore.Text = "0";
        }

        /// <summary>
        /// Method to load the scores from the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the LoadScores method to read scores from file and display them
            LoadScores();
        }

        /// <summary>
        /// Method to save the scores to the file from form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the DataGridView before saving
            dtgdHighscores.DataSource = null;

            // Clear DAO
            _minesweeperDAO.ClearScores();

            // Iterate through the list of scores from the form and add them to the DAO
            for (int i = 0; i < _playerScores.Count; i++)
            {
                // Get the score details from the current GameState object
                GameState score = _playerScores[i];

                // Get the Id
                int id = score.getId();
                // Get the Name
                string name = score.getName();
                // Get the Score
                int scoreValue = score.getScore();
                // Get the Date
                DateTime time = score.getDate();

                // Create a new GameState object with the retrieved details
                GameState newScore = new GameState(id, name, scoreValue, time.TimeOfDay);

                // Add the new GameState object to the DAO
                _minesweeperDAO.AddPlayerScore(score);
            }

            // Write scores to file
            _minesweeperDAO.WriteScoreToFile();

            // Now get the updated list and display it
            SetScores(_playerScores);
        }

        /// <summary>
        /// Method to set the scores in the DataGridView
        /// </summary>
        /// <param name="scores">List of GameState objects</param>
        public void SetScores(List<GameState> scores)
        {
            // Clear the DataGridView before setting new data
            dtgdHighscores.DataSource = null;
            // Set the DataGridView's data source to the provided list of scores
            dtgdHighscores.DataSource = scores;
            // Update the bar chart
            UpdateScoreChart();
        }

        /// <summary>
        /// Method to load the scores from the _minesweeperDAO and display them in the DataGridView
        /// </summary>
        public void LoadScores()
        {
            // Read scores from file into the DAO's list
            _minesweeperDAO.ReadScoresFromFile();
            // Get the list of scores from the DAO
            List<GameState> dummyList = _minesweeperDAO.GetScoresList();
            // Assign the list to the class level variable
            _playerScores = dummyList.ToList();
            // Get the updated list and display it
            SetScores(_playerScores);
        }

        /// <summary>
        /// Method to handle the click event for the exit dropdown menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        /// <summary>
        /// Method to handle the click event for the By Name dropdown menu item and sort the scores by name in ascending order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sort the scores by name in ascending order and set the sorted list to the DataGridView
            var sortedScores = _playerScores.OrderBy(s => s.getName()).ToList();
            // Set the sorted scores to the DataGridView
            SetScores(sortedScores);
        }

        /// <summary>
        /// Method to handle the click event for the By Score dropdown menu item and sort the scores by score in descending order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ByScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sort the scores by score in descending order and set the sorted list to the DataGridView
            var sortedScores = _playerScores.OrderByDescending(s => s.getScore()).ToList();
            // Set the sorted scores to the DataGridView
            SetScores(sortedScores);
        }

        /// <summary>
        /// Method to handle the click event for the By Date dropdown menu item and sort the scores by date in descending order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sort the scores by date in descending order and set the sorted list to the DataGridView
            var sortedScores = _playerScores.OrderByDescending(s => s.getDate()).ToList();
            // Set the sorted scores to the DataGridView
            SetScores(sortedScores);
        }

        /// <summary>
        /// Event handler for when the selection changes in the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtgdHighscores_SelectionChanged(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dtgdHighscores.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dtgdHighscores.SelectedRows[0];

                // Get the GameState object from the selected row
                GameState selectedScore = selectedRow.DataBoundItem as GameState;

                // Check if the selectedScore is not null before proceeding
                if (selectedScore != null)
                {
                    // Get the player name from the selected score
                    string playerName = selectedScore.getName();

                    // Calculate and display the average time and score for this player
                    CalculateAndDisplayAverages(playerName);
                }
            }
        }

        /// <summary>
        /// Calculate and display the average time and score for a specific player
        /// </summary>
        /// <param name="playerName">The name of the player to calculate averages for</param>
        private void CalculateAndDisplayAverages(string playerName)
        {
            // Filter the scores to get only the scores for the selected player
            var playerScoresFiltered = _playerScores.Where(s => s.getName() == playerName).ToList();

            // Check if there are any scores for this player
            if (playerScoresFiltered.Count > 0)
            {
                // Calculate average score
                double averageScore = playerScoresFiltered.Average(s => s.getScore());

                // Calculate average time from the average of the total seconds and convert it back to TimeSpan
                double averageTimeInSeconds = playerScoresFiltered.Average(s => s.getTime().TotalSeconds);
                // Convert the average time in seconds back to TimeSpan for display
                TimeSpan averageTime = TimeSpan.FromSeconds(averageTimeInSeconds);

                // Display the results
                lblPlayerName.Text = playerName + "'s";
                lblAverageTime.Text = averageTime.ToString(@"hh\:mm\:ss");
                lblAverageScore.Text = ((int)averageScore).ToString();
            }
            else
            {
                // Clear the labels if no scores found
                lblPlayerName.Text = string.Empty;
                lblAverageTime.Text = "00:00:00";
                lblAverageScore.Text = "0";
            }
        }

        /// <summary>
        /// Update the bar chart to display scores for all of the saved scores
        /// </summary>
        private void UpdateScoreChart()
        {
            // Clear the plot
            pltScores.Plot.Clear();

            // Check if there are any scores to display
            if (_playerScores == null || _playerScores.Count == 0)
            {
                // If not, refresh the plot to show an empty chart and return
                pltScores.Refresh();
                return;
            }

            // Grab the scores and their corresponding positions for the bar chart
            double[] scores = _playerScores.Select(s => (double)s.getScore()).ToArray();
            // Set the positions for the bars to be the index of each score in the list
            double[] positions = Enumerable.Range(0, scores.Length).Select(i => (double)i).ToArray();

            // Add bar plot
            var barPlot = pltScores.Plot.Add.Bars(positions, scores);

            // For each bar in the bar plot, set the fill color
            foreach (var bar in barPlot.Bars)
            {
                // Set the fill color of the bars to a specific shade of blue
                bar.FillColor = ScottPlot.Color.FromHex("#4472C4");
            }

            // Configure the plot
            pltScores.Plot.Title("Game Scores");
            pltScores.Plot.XLabel("Game Number");
            pltScores.Plot.YLabel("Score");

            // Set Y axis to start at 0
            pltScores.Plot.Axes.SetLimitsY(0, scores.Max() * 1.1);

            // Enable mouse to move around the plot
            // User can left click to drag around the plot, right click and drag to zoom, or use scroll wheel
            pltScores.UserInputProcessor.IsEnabled = true;

            // If there more than 20 scores, show only the first 20 initially
            if (scores.Length > 20)
            {
                // Set the X axis limits to show only the first 20 scores
                pltScores.Plot.Axes.SetLimitsX(-0.5, 19.5);
            }

            // Refresh the plot
            pltScores.Refresh();
        }
    }
}