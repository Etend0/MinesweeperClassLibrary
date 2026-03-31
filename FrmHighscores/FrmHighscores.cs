using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.MinesweeperDAO;
using static System.Windows.Forms.LinkLabel;

namespace FrmHighscores
{
    public partial class FrmHighscores : Form
    {
        // Create an instance of the MinesweeperDAO
        private MinesweeperDAO _minesweeperDAO = new MinesweeperDAO();

        // Class level variables
        private List<GameState> _playerScores;

        public FrmHighscores()
        {
            InitializeComponent();

            loadToolStripMenuItem.Click += LoadToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
        }

        /// <summary>
        /// Method to load the scores from the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Read scores from file into the DAO's list
            _minesweeperDAO.ReadScoresFromFile();
            // Get the updated list from the DAO
            _playerScores = _minesweeperDAO.GetScoresList();
            // Now get the updated list and display it
            SetScores(_playerScores);

            for (int i = 0; i < _playerScores.Count; i++)
            {
                GameState score = _playerScores[i];

                int id = score.getId();
                string name = score.getName();
                int scoreValue = score.getScore();
                DateTime time = score.getDate();

                MessageBox.Show($"ID: {id}, Name: {name}, Score: {scoreValue}, Time: {time}");
            }
        }

        /// <summary>
        /// Method to save the scores to the file from form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Safely unbind before modifying
            dtgdHighscores.DataSource = null;

            // Clear DAO and add everything from the current list
            _minesweeperDAO.ClearScores();

            for(int i = 0; i < _playerScores.Count; i++)
            {
                GameState score = _playerScores[i];

                int id = score.getId();
                string name = score.getName();
                int scoreValue = score.getScore();
                DateTime time = score.getDate();

                MessageBox.Show($"ID: {id}, Name: {name}, Score: {scoreValue}, Time: {time}");

                GameState newScore = new GameState(id, name, scoreValue, time.TimeOfDay);

                _minesweeperDAO.AddPlayerScore(score);
            }

            // Write scores to file
            _minesweeperDAO.WriteScoreToFile();

            _playerScores = _minesweeperDAO.GetScoresList();

            // Rebind so the grid shows the data again
            SetScores(_playerScores);
        }

        /// <summary>
        /// Set the scores in the DataGridView
        /// </summary>
        /// <param name="scores">List of GameState objects</param>
        public void SetScores(List<GameState> scores)
        {
            dtgdHighscores.DataSource = null;
            dtgdHighscores.DataSource = scores;
        }
    }
}