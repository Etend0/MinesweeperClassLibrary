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

        // Variable to track whether the player is using a reward
        private bool _useReward = false;

        // Hold the size of the button for use in resizing images when the graphics are changed
        private int _buttonSize;

        // Variable to track whether we need to set up the sprites so we don't do it more than once in SetupButtons
        private bool _setupSprites = true;

        // Dictionary to cache resized images for each tile type
        private Dictionary<string, Image> _tileImages;

        // Dictionary to store the original sprites for each tile type
        private Dictionary<string, Image> _originalSprites;

        /// <summary>
        /// MinesweeperGUIForm constructor
        /// </summary>
        public MinesweeperGUIForm()
        {
            InitializeComponent();

            // Register event handlers for save/load menu items
            saveGameToolStripMenuItem.Click += SaveGameToolStripMenuItem_Click;
            loadGameToolStripMenuItem.Click += LoadGameToolStripMenuItem_Click;

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

            // Hide the button for using reward
            btnUseReward.Visible = false;

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

            // Set the rewards
            lblRewards.Text = "0";

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

            if (_setupSprites)
            {
                // Declare and initialize
                // Calculate the size of each button based on
                // the panel width and the number of buttons needs
                _buttonSize = pnlMinesweeperBoard.Width / _board.Size;
                // Set the panel to be square
                pnlMinesweeperBoard.Height = pnlMinesweeperBoard.Width;

                // Cache resized images for all tile types
                _defButtonSize = _buttonSize;

                // Initialize the dictionary with resized images for each tile type
                _tileImages = new Dictionary<string, Image>
                {
                    ["B"] = ResizeImage(Properties.Resources.Skull, _buttonSize, _buttonSize, false),
                    ["F"] = ResizeImage(Properties.Resources.Flag2, _buttonSize, _buttonSize, false),
                    ["R"] = ResizeImage(Properties.Resources.Gold, _buttonSize, _buttonSize, false),
                    ["?"] = ResizeImage(Properties.Resources.Tile_1, _buttonSize, _buttonSize, false),
                    ["."] = ResizeImage(Properties.Resources.Tile_Flat, _buttonSize, _buttonSize, false),
                    ["1"] = ResizeImage(Properties.Resources.Number_1, _buttonSize, _buttonSize, false),
                    ["2"] = ResizeImage(Properties.Resources.Number_2, _buttonSize, _buttonSize, false),
                    ["3"] = ResizeImage(Properties.Resources.Number_3, _buttonSize, _buttonSize, false),
                    ["4"] = ResizeImage(Properties.Resources.Number_4, _buttonSize, _buttonSize, false),
                    ["5"] = ResizeImage(Properties.Resources.Number_5, _buttonSize, _buttonSize, false),
                    ["6"] = ResizeImage(Properties.Resources.Number_6, _buttonSize, _buttonSize, false),
                    ["7"] = ResizeImage(Properties.Resources.Number_7, _buttonSize, _buttonSize, false),
                    ["8"] = ResizeImage(Properties.Resources.Number_8, _buttonSize, _buttonSize, false)
                };
                // Mark that we have set up the sprites so we don't do it again in this method
                _setupSprites = false;
            }

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
                    button.Width = _buttonSize;
                    button.Height = _buttonSize;
                    // Set the location of the button
                    // using the left and top sides
                    button.Left = row * _buttonSize;
                    button.Top = col * _buttonSize;

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
                    if (!_useReward)
                    {
                        // Update the cell in the board model to be visited
                        _minesweeperLogic.UpdateCell(row, col, 1);
                        // Set the selected cell to the cell that was just updated
                        _selectedCell = _board.Cells[col, row];
                    }
                    else
                    {
                        // If the user is using a reward, we want to reveal the cell without marking it as visited
                        _selectedCell = _board.Cells[col, row];
                        // Check if the selected cell is a bomb
                        if (_selectedCell.isBomb)
                        {
                            // If it is a bomb, we want to update the cell to be revealed but not visited
                            MessageBox.Show("This is a bomb!");
                        }
                        else
                        {
                            // If it is not a bomb, we want to update the cell to be revealed but not visited
                            MessageBox.Show("This is not a bomb.");
                        }
                        // Set use reward back to false after using it
                        _useReward = false;
                    }
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

                // Check if we have rewards remaining after the user's move
                if (_minesweeperLogic.RewardsRemaining > 0)
                {
                    // If we have rewards remaining, show the button to use a reward
                    btnUseReward.Visible = true;
                }
                else
                {
                    // If we don't have any rewards remaining, hide the button to use a reward
                    btnUseReward.Visible = false;
                }

                // Update the rewards label to show the number of rewards remaining
                lblRewards.Text = _minesweeperLogic.RewardsRemaining.ToString();
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
        /// <param name="changeScale"></param>
        /// <returns></returns>
        private Image ResizeImage(Image img, int width, int height, bool changeScale)
        {
            // Create a new Bitmap with the set width and height
            Bitmap bmp = new Bitmap(width, height);
            // Use a Graphics object to draw the original image onto the new Bitmap with the specified size
            using (Graphics g = Graphics.FromImage(bmp))
            {
                if (!changeScale)
                {
                    // Set the interpolation mode to high quality to make the resized image look better
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                }
                else
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                }
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
            // Reset the rewards counter
            _minesweeperLogic.ResetRewards();
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
            // Set the rewards label to 0
            lblRewards.Text = "0";
            // Set the rewards button not be visible
            btnUseReward.Visible = false;

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

        /// <summary>
        /// Method to allow the user to use their reward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnUseRewardEH(object? sender, EventArgs e)
        {
            if (!_victory && !_death)
            {
                // Set the use reward variable to true
                _useReward = true;
                // Decrement the rewards remaining in the game logic
                _minesweeperLogic.DecrementRewards();
                // Update the rewards label to show the number of rewards remaining
                lblRewards.Text = _minesweeperLogic.RewardsRemaining.ToString(); 
            }
        }

        /// <summary>
        /// Method to switch the graphics to the default sprites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Initialize the dictionary with resized images for each tile type
            _tileImages = new Dictionary<string, Image>
            {
                ["B"] = ResizeImage(Properties.Resources.Skull, _buttonSize, _buttonSize, false),
                ["F"] = ResizeImage(Properties.Resources.Flag2, _buttonSize, _buttonSize, false),
                ["R"] = ResizeImage(Properties.Resources.Gold, _buttonSize, _buttonSize, false),
                ["?"] = ResizeImage(Properties.Resources.Tile_1, _buttonSize, _buttonSize, false),
                ["."] = ResizeImage(Properties.Resources.Tile_Flat, _buttonSize, _buttonSize, false),
                ["1"] = ResizeImage(Properties.Resources.Number_1, _buttonSize, _buttonSize, false),
                ["2"] = ResizeImage(Properties.Resources.Number_2, _buttonSize, _buttonSize, false),
                ["3"] = ResizeImage(Properties.Resources.Number_3, _buttonSize, _buttonSize, false),
                ["4"] = ResizeImage(Properties.Resources.Number_4, _buttonSize, _buttonSize, false),
                ["5"] = ResizeImage(Properties.Resources.Number_5, _buttonSize, _buttonSize, false),
                ["6"] = ResizeImage(Properties.Resources.Number_6, _buttonSize, _buttonSize, false),
                ["7"] = ResizeImage(Properties.Resources.Number_7, _buttonSize, _buttonSize, false),
                ["8"] = ResizeImage(Properties.Resources.Number_8, _buttonSize, _buttonSize, false)
            };
            // Update the buttons to reflect the change in images
            UpdateButtons();
        }

        /// <summary>
        /// Method to switch the graphics to the original sprites from the classic minesweeper game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void classicToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Update the tile images to the original sprites
            _tileImages = new Dictionary<string, Image>
            {
                ["B"] = ResizeImage(Properties.Resources.OGBomb, _buttonSize, _buttonSize, true),
                ["F"] = ResizeImage(Properties.Resources.OGFlag, _buttonSize, _buttonSize, true),
                ["R"] = ResizeImage(Properties.Resources.WinMine, _buttonSize, _buttonSize, true),
                ["?"] = ResizeImage(Properties.Resources.OGHidden, _buttonSize, _buttonSize, true),
                ["."] = ResizeImage(Properties.Resources.OGRevealed, _buttonSize, _buttonSize, true),
                ["1"] = ResizeImage(Properties.Resources.OG1, _buttonSize, _buttonSize, true),
                ["2"] = ResizeImage(Properties.Resources.OG2, _buttonSize, _buttonSize, true),
                ["3"] = ResizeImage(Properties.Resources.OG3, _buttonSize, _buttonSize, true),
                ["4"] = ResizeImage(Properties.Resources.OG4, _buttonSize, _buttonSize, true),
                ["5"] = ResizeImage(Properties.Resources.OG5, _buttonSize, _buttonSize, true),
                ["6"] = ResizeImage(Properties.Resources.OG6, _buttonSize, _buttonSize, true),
                ["7"] = ResizeImage(Properties.Resources.OG7, _buttonSize, _buttonSize, true),
                ["8"] = ResizeImage(Properties.Resources.OG8, _buttonSize, _buttonSize, true)
            };
            // Update the buttons to reflect the change in images
            UpdateButtons();
        }

        /// <summary>
        /// Event handler for Save Game menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGameToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Don't allow saving if game is already over
            if (_victory || _death)
            {
                MessageBox.Show("Cannot save a completed game!", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create a SaveFileDialog to let the user choose where to save
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Minesweeper Save Files (*.minesave)|*.minesave|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "minesave";
                saveFileDialog.Title = "Save Game";
                saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                saveFileDialog.FileName = $"MinesweeperSave_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Create a GameSave object with the current game state
                        GameSave gameSave = new GameSave(
                            _board,
                            _elapsedSeconds,
                            _gameDifficulty,
                            _gameState,
                            _victory,
                            _death,
                            _minesweeperLogic.RewardsRemaining
                        );

                        // Save the game using the MinesweeperDAO
                        bool success = _minesweeperDAO.SaveGame(gameSave, saveFileDialog.FileName);

                        if (success)
                        {
                            MessageBox.Show("Game saved successfully!", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save game.\n\nError: {ex.Message}", "Save Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for Load Game menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGameToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            // Create an OpenFileDialog to let the user choose which save to load
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Minesweeper Save Files (*.minesave)|*.minesave|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                openFileDialog.DefaultExt = "minesave";
                openFileDialog.Title = "Load Game";
                openFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Ask user for confirmation
                    DialogResult result = MessageBox.Show(
                        "Loading a saved game will replace the current game. Continue?",
                        "Load Game",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            // Load the game using the MinesweeperDAO
                            GameSave gameSave = _minesweeperDAO.LoadGame(openFileDialog.FileName);

                            if (gameSave != null)
                            {
                                // Stop the current timer
                                gameTimer.Stop();

                                // Restore the game state
                                _board = gameSave.Board;
                                _elapsedSeconds = gameSave.ElapsedSeconds;
                                _gameDifficulty = gameSave.GameDifficulty;
                                _gameState = gameSave.GameState;
                                _victory = gameSave.Victory;
                                _death = gameSave.Death;

                                // Update the MinesweeperLogic with the loaded board
                                _minesweeperLogic.GetBoard(_board);
                                _minesweeperLogic.GetSize(_board.Size);

                                // Clear the panel and recreate buttons
                                pnlMinesweeperBoard.Controls.Clear();
                                SetUpButtons();

                                // Update all buttons to show current state
                                UpdateButtons();

                                // Update the labels
                                TimeSpan time = TimeSpan.FromSeconds(_elapsedSeconds);
                                lblStartTime.Text = time.ToString(@"hh\:mm\:ss");
                                lblRewards.Text = gameSave.RewardsRemaining.ToString();

                                // Show/hide reward button based on rewards remaining
                                if (gameSave.RewardsRemaining > 0)
                                {
                                    btnUseReward.Visible = true;
                                }
                                else
                                {
                                    btnUseReward.Visible = false;
                                }

                                // Restart the timer if game is not over
                                if (!_victory && !_death)
                                {
                                    gameTimer.Start();
                                }

                                MessageBox.Show("Game loaded successfully!", "Load Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to load game. The save file may be corrupted.", "Load Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to load game.\n\nError: {ex.Message}", "Load Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}