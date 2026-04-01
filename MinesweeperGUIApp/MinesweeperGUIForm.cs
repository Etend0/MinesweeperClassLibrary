using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using StartNewGameGUI;
using FrmGetUser;
using FrmHighscores;
using MinesweeperClassLibrary.Services.MinesweeperDAO;

namespace MinesweeperGUIApp
{
    public partial class MinesweeperGUIForm : Form
    {
        // Create an instance of the IMinesweeperLogic
        private IMinesweeperLogic _minesweeperLogic = new MinesweeperLogic();

        // Create an instance of the NewGameForm
        public NewGameForm _newGameForm = new NewGameForm();

        // Create an instance of the FrmGetName form
        public FrmGetName _getNameForm = new FrmGetName();

        // Create an instance of the FrmHighscores form
        public FrmHighscores.FrmHighscores _highscoresForm = new FrmHighscores.FrmHighscores();

        // Create an instance of the MinesweeperDAO
        private MinesweeperDAO _minesweeperDAO = new MinesweeperDAO();

        // Create a new board with difficulty level 1
        private BoardModel _board;

        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        // Object to represent the player at the end of the game
        private GameState _player;

        // Variable to track the current difficulty level of the game
        private int _gameDifficulty;

        // Variable to track the currently selected cell
        private CellModel _selectedCell;

        // Variable to track the current state of the game (still playing, won, or lost)
        private string _gameState;

        // Variable to track whether the player has won the game
        private bool _victory;

        // Variable to track whether the player has lost the game
        private bool _death;

        // Timer to track the elapsed time of the game
        private System.Windows.Forms.Timer gameTimer;

        // Variable to track the elapsed time in seconds
        private int _elapsedSeconds = 0;

        // Variable to store the default button size for resizing images
        private int _defButtonSize;

        // Dictionary to cache resized images for each tile type
        private Dictionary<string, Image> _tileImages;

        /// <summary>
        /// MinesweeperGUIForm constructor
        /// </summary>
        public MinesweeperGUIForm()
        {
            InitializeComponent();

            // Show the difficulty form
            _newGameForm.ShowDialog();

            // Get the difficulty level from the form
            _newGameForm.GetDifficultyLevel(out _gameDifficulty);

            // Initialize the board (replace 0 with the selected difficulty from _newGameForm)
            _board = new BoardModel(_gameDifficulty);

            // Read the scores from the file using the DAO to populate the highscores form with scores if there are any
            _minesweeperDAO.ReadScoresFromFile();

            // Set the board model for the MinesweeperLogic
            _minesweeperLogic.GetBoard(_board);
            // Get the size of the board
            _minesweeperLogic.GetSize(_board.Size);
            // Set up the rewards on the board
            _minesweeperLogic.SetupRewards(_board.Cells, 0.02);
            // Set up the bombs on the board
            _minesweeperLogic.SetupBombs(_board.Cells, 0.15);
            // Count the bombs on the board
            _minesweeperLogic.CountBombs();
            // Set the game state to empty
            _gameState = " ";
            // Set victory to false
            _victory = false;
            // Set death to false
            _death = false;

            // Initialize the game timer
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer;

            // Set text to 0
            lblStartTime.Text = "00:00:00";
            lblStartTime.AutoSize = true;
            this.Controls.Add(lblStartTime);

            // Set the score
            lblScore.Text = "0";

            // Set up the buttons on the board
            SetUpButtons();

            // Set timer to 0
            _elapsedSeconds = 0;

            // Start the timer
            gameTimer.Start();
        }

        /// <summary>
        /// Populate the panel control with buttons
        /// </summary>
        private void SetUpButtons()
        {
            // Initialize the 2D button array
            _buttons = new Button[_board.Size, _board.Size];

            // Declare and initialize
            // Calculate the size of each button based on
            // the panel width and the number of buttons needs
            int buttonSize = pnlMinesweeperBoard.Width / _board.Size;
            // Set the panel to be square
            pnlMinesweeperBoard.Height = pnlMinesweeperBoard.Width;

            // Cache resized images for all tile types
            _defButtonSize = buttonSize;

            // Initialize the dictionary with resized images for each tile type
            _tileImages = new Dictionary<string, Image>
            {
                ["B"] = ResizeImage(Properties.Resources.Skull, buttonSize, buttonSize),
                ["F"] = ResizeImage(Properties.Resources.Flag2, buttonSize, buttonSize),
                ["R"] = ResizeImage(Properties.Resources.Gold, buttonSize, buttonSize),
                ["?"] = ResizeImage(Properties.Resources.Tile_1, buttonSize, buttonSize),
                ["."] = ResizeImage(Properties.Resources.Tile_Flat, buttonSize, buttonSize),
                ["1"] = ResizeImage(Properties.Resources.Number_1, buttonSize, buttonSize),
                ["2"] = ResizeImage(Properties.Resources.Number_2, buttonSize, buttonSize),
                ["3"] = ResizeImage(Properties.Resources.Number_3, buttonSize, buttonSize),
                ["4"] = ResizeImage(Properties.Resources.Number_4, buttonSize, buttonSize),
                ["5"] = ResizeImage(Properties.Resources.Number_5, buttonSize, buttonSize),
                ["6"] = ResizeImage(Properties.Resources.Number_6, buttonSize, buttonSize),
                ["7"] = ResizeImage(Properties.Resources.Number_7, buttonSize, buttonSize),
                ["8"] = ResizeImage(Properties.Resources.Number_8, buttonSize, buttonSize)
            };

            // Use nested for loops to loop through the boards Grid
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    // Set up each individual button
                    // Create a new button in the 2D array
                    _buttons[row, col] = new Button();
                    // Get the current button
                    Button button = _buttons[row, col];
                    // Set the size for the button
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    // Set the location of the button
                    // using the left and top sides
                    button.Left = row * buttonSize;
                    button.Top = col * buttonSize;

                    // Attach a click event handler to the button
                    button.MouseDown += BtnSquareClickEH;

                    // Store the location of the button in
                    // the Tag property using a Point object
                    button.Tag = new Point(row, col);
                    // Add the button to the panel's controls
                    pnlMinesweeperBoard.Controls.Add(_buttons[row, col]);
                    // Set the background image for the button
                    _buttons[row, col].Image = _tileImages["?"];
                }
            }

        } // End of SetUpButtons method

        /// <summary>
        /// Mouse down event handler for the board buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSquareClickEH(object? sender, MouseEventArgs e)
        {
            // Only allow the user to interact with the board if they haven't won or lost yet
            if (!_victory && !_death)
            {
                // Declare and initialize
                Button button = (Button)sender;
                Point point = (Point)button.Tag;
                int row = point.X;
                int col = point.Y;

                // If we clicked left mouse button, we want to reveal the cell
                if (e.Button == MouseButtons.Left)
                {
                    // Update the cell in the board model to be visited
                    _minesweeperLogic.UpdateCell(row, col, 1);
                    // Set the selected cell to the cell that was just updated
                    _selectedCell = _board.Cells[col, row];
                }
                // If we clicked the right mouse button, we want to flag or unflag the cell
                else if (e.Button == MouseButtons.Right)
                {
                    // Get the selected cell
                    _selectedCell = _board.Cells[col, row];

                    // Check if it is flagged
                    if (_selectedCell.isFlagged)
                    {
                        // If it is flagged, we want to unflag it
                        _minesweeperLogic.UpdateCell(row, col, 0);
                    }
                    else
                    {
                        // If it is not flagged, we want to flag it
                        _minesweeperLogic.UpdateCell(row, col, 2);
                    }
                }

                // Update the buttons
                UpdateButtons();
            }
        }

        /// <summary>
        /// Update the text for each button based on the board
        /// </summary>
        private void UpdateButtons()
        {
            // Declare and initialize
            string piece;

            // Loop through each cell in the grid to update the corresponding button
            for (int row = 0; row < _board.Size; row++)
            {
                // Loop through each column in the current row
                for (int col = 0; col < _board.Size; col++)
                {
                    // Get the current cell
                    _selectedCell = _board.Cells[col, row];

                    // Call the GetTileImage method to get the image for the current cell
                    _buttons[row, col].Image = GetTileImage(_selectedCell, _buttons[row, col].Image);
                }
            }

            // Determine the game state after the user's move
            _gameState = _board.DetermineGameState(_gameState);

            // Check the game state for whether we have won, lost, or are still playing
            switch (_gameState)
            {
                // If we are still playing, we don't need to do anything
                case "StillPlaying":
                    break;

                // If we have won, set victory to true
                case "Won":
                    // Set victory to true
                    _victory = true;
                    // Stop the timer
                    gameTimer.Stop();
                    // Set the score to 0 to initialize
                    int score = 0;
                    // Show a message box with the score by using the CalculateScore method
                    //System.Windows.Forms.MessageBox.Show("Congratulations! You won! Your score is " + CalculateScore(score));
                    // Set the score label to the calculated score
                    _getNameForm.GetInt(CalculateScore(score));
                    // Show the get name form to enter the user's name and display the score
                    _getNameForm.ShowDialog();
                    GameState gameState = new GameState(0, _getNameForm.returnString(_getNameForm.Name), CalculateScore(score), TimeSpan.FromSeconds(_elapsedSeconds));
                    _minesweeperDAO.AddPlayerScore(gameState);
                    _minesweeperDAO.WriteScoreToFile();
                    break;

                // If we have lost, set death to true
                case "Lost":
                    // Set death to true
                    _death = true;
                    // Stop the timer
                    gameTimer.Stop();
                    // Output the losing message
                    System.Windows.Forms.MessageBox.Show("* KABOOM! * Sorry, you lost!");
                    break;
            }
        } // End of UpdateButtons method

        /// <summary>
        /// Method to track the game time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimer(object sender, EventArgs e)
        {
            // Increment the time
            _elapsedSeconds++;
            // Get the number of seconds
            TimeSpan time = TimeSpan.FromSeconds(_elapsedSeconds);
            // Set the label to the elapesed time
            lblStartTime.Text = time.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Method to calculate the score
        /// </summary>
        /// <param name="score"></param>
        private int CalculateScore(int score)
        {
            // Set the base score
            int baseScore = 5000;
            // Get the size of the board
            int size = _board.Size;
            // Get the difficulty multiplier based on the size of the board
            int difficultyMultiplier = _minesweeperLogic.Size;
            // Calculate the score by multiplying the base score by the size and difficultyMultiplier, then divide by the elapsed time and add 1 to avoid dividing by zero
            int tallyScore = (int)((baseScore * size * difficultyMultiplier) / (_elapsedSeconds + 1));
            // Set the score to the calculated score
            score = tallyScore;
            // Return the score
            return score;
        }

        /// <summary>
        /// Method to set the size of the image to the buttons
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Image ResizeImage(Image img, int width, int height)
        {
            // Create a new Bitmap with the set width and height
            Bitmap bmp = new Bitmap(width, height);
            // Use a Graphics object to draw the original image onto the new Bitmap with the specified size
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Set the interpolation mode to high quality to make the resized image look better
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // Set image into the bitmap with the new size
                g.DrawImage(img, 0, 0, width, height);
            }
            // Return the resized image
            return bmp;
        }

        /// <summary>
        /// Method to get the image for a given cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        private Image GetTileImage(CellModel cell, Image image)
        {
            // Grab the cell type
            string piece = cell.DrawMe().Trim();

            // Try to get the image from the dictionary based on the cell type
            if (_tileImages.TryGetValue(piece, out var img))
                return img;

            // Default to flat tile if not found
            return _tileImages["."];
        }

        /// <summary>
        /// Button click event to reset the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResetGameEH(object? sender, EventArgs e)
        {
            // Stop the timer
            gameTimer.Stop();

            // Re-initialize the board and the game logic
            _board = new BoardModel(_gameDifficulty);
            // Set the board model for the MinesweeperLogic
            _minesweeperLogic.GetBoard(_board);
            // Get the size of the board
            _minesweeperLogic.GetSize(_board.Size);
            // Set up the rewards on the board
            _minesweeperLogic.SetupRewards(_board.Cells, 0.02);
            // Set up the bombs on the board
            _minesweeperLogic.SetupBombs(_board.Cells, 0.15);
            // Count the bombs on the board
            _minesweeperLogic.CountBombs();

            // Reset game state
            _gameState = " ";
            // Set victory to false
            _victory = false;
            // Set death to false
            _death = false;
            // Set the elapsed seconds to 0
            _elapsedSeconds = 0;
            // Set the start time text to the default format
            lblStartTime.Text = "00:00:00";
            // Set the score label to 0
            lblScore.Text = "0";

            // Remove old buttons from the panel
            pnlMinesweeperBoard.Controls.Clear();

            // Setup buttons on the board
            SetUpButtons();

            // Start the timer
            gameTimer.Start();
        }

        /// <summary>
        /// Method to show the highscores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnHighscoresEH(object? sender, EventArgs e)
        {
            // Call the show highscores method if user clicks the button to show highscores
            ShowHighscores();
        }

        /// <summary>
        /// Show the highscores in the FrmHighscores data grid
        /// </summary>
        public void ShowHighscores()
        {
            // Read the scores from the file using the DAO
            _minesweeperDAO.ReadScoresFromFile();
            // Get the form to load the scores into the data grid
            _highscoresForm.LoadScores();
            // Show the highscores form
            _highscoresForm.ShowDialog();
        }
    }
}