using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using StartNewGameGUI;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;

namespace MinesweeperGUIApp
{
    public partial class MinesweeperGUIForm : Form
    {
        // Create an instance of the MinesweeperLogic
        private IMinesweeperLogic _minesweeperLogic = new MinesweeperLogic();

        // Create an instance of the NewGameForm
        public NewGameForm _newGameForm = new NewGameForm();

        // Create a new board with difficulty level 1
        private BoardModel _board;

        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        // Variable to track the current difficulty level of the game
        private int _gameDifficulty;

        // Variable to track the currently selected cell
        private CellModel _selectedCell;

        // Variable to track the current state of the game (still playing, won, or lost)
        private string _gameState;

        private bool _victory;

        private bool _death;

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

            // Set the board model for the MinesweeperLogic
            _minesweeperLogic.GetBoard(_board);
            // Get the size of the board
            _minesweeperLogic.GetSize(_board.Size);
            // Set up the rewards on the board
            _minesweeperLogic.SetupRewards(_board.Cells, 0.02);
            // Set up the bombs on the board
            _minesweeperLogic.SetupBombs(_board.Cells, 0.07);
            // Count the bombs on the board
            _minesweeperLogic.CountBombs();

            _gameState = " ";

            _victory = false;

            _death = false;

            // Set up the buttons on the board
            SetUpButtons();
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
                    _buttons[row, col].BackColor = Color.Gray;
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
            if (!_victory && !_death)
            {
                // Declare and initialize
                Button button = (Button)sender;
                Point point = (Point)button.Tag;
                int row = point.X;
                int col = point.Y;

                if (e.Button == MouseButtons.Left)
                {
                    _minesweeperLogic.UpdateCell(row, col, 1);
                    _selectedCell = _board.Cells[col, row];
                    string piece = _selectedCell.DrawMe();
                    _buttons[row, col].BackColor = Color.White;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    _selectedCell = _board.Cells[col, row];

                    if (_selectedCell.isFlagged)
                    {
                        _minesweeperLogic.UpdateCell(row, col, 0);
                        _buttons[row, col].BackColor = Color.Gray;
                        _buttons[row, col].Text = "";
                    }
                    else
                    {
                        _minesweeperLogic.UpdateCell(row, col, 2);
                        _buttons[row, col].ForeColor = Color.Red;
                        _selectedCell = _board.Cells[col, row];
                        string piece = _selectedCell.DrawMe();
                        _buttons[row, col].Text = piece;
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
                for (int col = 0; col < _board.Size; col++)
                {
                    _selectedCell = _board.Cells[col, row];

                    if (_selectedCell.isVisited)
                    {
                        _buttons[row, col].BackColor = Color.White;

                        piece = _selectedCell.DrawMe();

                        if (piece != "? " && piece != ". " && piece != " ")
                        {
                            _buttons[row, col].ForeColor = Color.Black;
                            // Update the text for the button
                            _buttons[row, col].Text = piece;
                        }
                        else
                        {
                            // Clear any stale text (e.g., flag cleared by flood fill)
                            _buttons[row, col].Text = "";
                        }
                    }
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
                    System.Windows.Forms.MessageBox.Show("Congratulations! You won! Your score is ");
                    break;

                // If we have lost, set death to true
                case "Lost":
                    // Set death to true
                    _death = true;
                    System.Windows.Forms.MessageBox.Show("* KABOOM! * Sorry, you lost!");
                    break;
            }
        } // End of UpdateButtons method
    }
}
