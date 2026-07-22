using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MinesweeperClassLibrary.Models.BoardModel;
using static System.Formats.Asn1.AsnWriter;

/*
 * Elijah Hodge
 * CST - 250
 * 07/21/2026
 * Minesweeper Class Library
 * Milestone 4
 */

namespace MinesweeperGUIApp
{
    public partial class GameForm : Form
    {
        // Declare and initialize
        // Board model to hold the board data
        private BoardModel _board;

        // Minesweeper logic to handle the game logic
        private MinesweeperLogic _minesweeperLogic = new MinesweeperLogic();

        // Timer to track the elapsed time of the game
        private System.Windows.Forms.Timer gameTimer;

        // Variable to track the elapsed time in seconds
        private int _elapsedSeconds = 0;

        // Variable to store the default button size for resizing images
        private int _defButtonSize;

        // Dictionary to cache resized images for each tile type
        private Dictionary<string, Image> _tileImages;

        // Variable to track the currently selected cell
        private CellModel _selectedCell;

        // Variable to store the difficulty level of the game
        private int _difficultyLevel;

        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        // Variable to track if the player has won the game
        static private bool _victory;

        // Variable to track if the player has lost the game
        static private bool _death;

        // Variable to track if the player has flagged all bombs
        static private bool _flaggedAllBombs;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the difficulty level
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public void SetDifficultyLevel(int difficultyLevel)
        {
            _difficultyLevel = difficultyLevel;
            // Create a new board with the given difficulty level
            _board = new BoardModel(_difficultyLevel);
            // Create a new instance of the MinesweeperLogic class
            _minesweeperLogic = new MinesweeperLogic();
            // Get the size of the board
            _minesweeperLogic.GetSize(_board.Size);
            // Set up the rewards on the board
            _minesweeperLogic.SetupRewards(_board.Cells);
            // Set up the bombs on the board
            _minesweeperLogic.SetupBombs(_board.Cells);
            // Count the bombs on the board
            _minesweeperLogic.CountBombs();

            // Initialize the game timer
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer;

            // Set text to 0
            lblTimer.Text = "00:00:00";
            lblTimer.AutoSize = true;
            this.Controls.Add(lblTimer);

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
            lblTimer.Text = time.ToString(@"hh\:mm\:ss");
        }

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

                CellModel currentCell = _board.Cells[row, col];

                // If we clicked left mouse button, we want to reveal the cell
                if (e.Button == MouseButtons.Left)
                {
                    if (currentCell.HasSpecialReward && !currentCell.IsVisited)
                    {
                        // If the cell has a special reward, increment the number of found rewards
                        _board.RewardsRemaining++;
                    }

                    if (currentCell.IsBomb == true || currentCell.HasSpecialReward == true || currentCell.NumberOfBombNeighbors > 0)
                    {
                        // If the user wants to check the cell, set IsVisited to true
                        currentCell.IsVisited = true;
                    }
                    else
                    {
                        // Call the boards flood fill method to reveal cells that aren't bombs, rewards or already revealed
                        _board.FloodFill(_board, currentCell.Row, currentCell.Column);
                    }

                    // If we for some reason click on a flagged cell, we want to unflag it
                    if (currentCell.IsFlagged)
                    {
                        currentCell.IsFlagged = false;
                    }
                }
                // If we clicked the right mouse button, we want to flag or unflag the cell
                else if (e.Button == MouseButtons.Right)
                {
                    if (!currentCell.IsVisited)
                    {
                        currentCell.IsFlagged = !currentCell.IsFlagged;

                        if (currentCell.IsFlagged)
                        {
                            // If the cell is flagged, increment the number of flags placed
                            _board.flagsPlaced++;
                        }
                        else
                        {
                            // If the cell is unflagged, decrement the number of flags placed
                            _board.flagsPlaced--;
                        }

                        // Immediate visual feedback for this single button
                        _buttons[row, col].Image = currentCell.IsFlagged ? _tileImages["F"] : _tileImages["?"];
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
                    _selectedCell = _board.Cells[row, col];

                    if (_selectedCell.IsVisited)
                    {
                        // Call the GetTileImage method to get the image for the current cell
                        _buttons[row, col].Image = GetTileImage(_selectedCell, _buttons[row, col].Image);
                    }
                }
            }

            // Determine the game state after the user's move
            (_victory, _death, _flaggedAllBombs) = _board.DetermineGameState();

            // Display the final game state to the user
            if (_victory && !_death)
            {
                // Check if the player has flagged all bombs
                if (!_flaggedAllBombs)
                {
                    // Stop the timer
                    gameTimer.Stop();
                    // Set the score to 0 to initialize
                    int score = 0;
                    // Show a message box with the score by using the CalculateScore method
                    System.Windows.Forms.MessageBox.Show("Congratulations! You won!");
                }
                else
                {
                    // Stop the timer
                    gameTimer.Stop();
                    // Set the score to 0 to initialize
                    int score = 0;
                    // Show a message box with the score by using the CalculateScore method
                    System.Windows.Forms.MessageBox.Show("Congratulations, found all the bombs!");
                }
            }
            else
            {
                // Check if the player has lost the game
                if (_death)
                {
                    // Stop the timer
                    gameTimer.Stop();
                    // Output the losing message
                    System.Windows.Forms.MessageBox.Show("* KABOOM! * Sorry, you lost!");
                }
            }
        } // End of UpdateButtons method

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
            string piece = cell.ToString().Trim();

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
            // Set the victory and death flags to false
            _victory = false;
            _death = false;

            // Stop the timer
            gameTimer.Stop();

            // Remove old buttons from the panel
            pnlMinesweeperBoard.Controls.Clear();

            // Create a new board with the same difficulty level
            SetDifficultyLevel(_difficultyLevel);
        }
    }
}
