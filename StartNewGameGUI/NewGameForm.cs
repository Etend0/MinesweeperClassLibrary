namespace StartNewGameGUI
{
    public partial class NewGameForm : Form
    {
        // Declare classd variables
        private String _difficulty;

        private int _difficultyLevel;

        // Boolean variable to check if difficulty level is set
        private bool _notSet = true;

        /// <summary>
        /// NewGameForm constructor
        /// </summary>
        public NewGameForm()
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
                // Set the current crust to the pizzas crust
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
                this.Close();
            }
            else
            {
                // Show a message box to select a difficulty
                MessageBox.Show("Please select a difficulty to start the game.");
            }
        }

        /// <summary>
        /// Grad the diffictuly level and return it
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void GetDifficultyLevel(out int difficultyLevel)
        {
            // Set the difficulty level to the class level variable
            difficultyLevel = _difficultyLevel;
        }
    }
}
