namespace MinesweeperGUIApp
{
    public partial class GameDifficultyForm : Form
    {
        // Declare class variables
        private String _difficulty;

        private int _difficultyLevel;

        // Boolean variable to check if difficulty level is set
        private bool _notSet = true;

        public GameForm gameForm;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameDifficultyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check if the difficulty is changed and set the difficulty variable to the text of the radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdoDifficultyCheckChangedEH(object sender, EventArgs e)
        {
            // Get the radio button from the sender object
            RadioButton radioButton = sender as RadioButton;

            // Make sure the radio button is not null
            if (radioButton != null && radioButton.Checked)
            {
                // Set the difficulty variable to the text of the radio button
                _difficulty = radioButton.Text;
            }
        }

        /// <summary>
        /// If the user selects play, check if the difficulty is not null and set the difficulty level based on the difficulty string and close the form. If the difficulty is null, show a message box to select a difficulty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartGameClickEH(object sender, EventArgs e)
        {
            // Check if the difficulty is not null
            if (_difficulty != null)
            {
                // Set the difficulty level based on the difficulty string
                switch (_difficulty)
                {
                    case "Very Easy":
                        _difficultyLevel = 0;
                        break;
                    case "Easy":
                        _difficultyLevel = 1;
                        break;
                    case "Medium":
                        _difficultyLevel = 2;
                        break;
                    case "Hard":
                        _difficultyLevel = 3;
                        break;
                }

                // Set the notSet variable to false
                _notSet = false;

                // Close the form
                this.Hide();

                // Create the GameForm if needed and wire the FormClosed handler so that when the game window closes
                // we also close the difficulty form.
                if (gameForm == null || gameForm.IsDisposed)
                {
                    gameForm = new GameForm();
                    gameForm.FormClosed += GameForm_FormClosed;
                }

                gameForm.SetDifficultyLevel(_difficultyLevel);

                gameForm.Show();

                if (gameForm == null || gameForm.IsDisposed)
                {
                    this.Close();
                }
            }
            else
            {
                // Show a message box to select a difficulty
                MessageBox.Show("Please select a difficulty to start the game.");
            }
        }

        /// <summary>
        /// Grab the diffictuly level and return it
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void GetDifficultyLevel(out int difficultyLevel)
        {
            // Set the difficulty level to the class level variable
            difficultyLevel = _difficultyLevel;
        }

        /// <summary>
        /// Called when the GameForm is closed — ensure the difficulty form is closed too.
        /// </summary>
        private void GameForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // If this form is hidden, close it; otherwise it stays in a consistent state.
            if (!this.IsDisposed)
            {
                // Close will dispose the form and allow the app to fully terminate if no other forms are open.
                this.Close();
            }
        }
    }
}
