using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using StartNewGameGUI;

namespace MinesweeperGUIApp
{
    public partial class MineseeperGUIForm : Form
    {
        // Create an instance of the MinesweeperLogic
        private IMinesweeperLogic minesweeperLogic = new MinesweeperLogic();

        public NewGameForm _newGameForm = new NewGameForm();

        // Create a new board with difficulty level 1
        private BoardModel _board;

        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        private int _gameDifficulty;

        /// <summary>
        /// MinesweeperGUIForm constructor
        /// </summary>
        public MineseeperGUIForm()
        {
            InitializeComponent();

            _newGameForm.ShowDialog();

            _newGameForm.GetDifficultyLevel(out _gameDifficulty);

            // Initialize the board (replace 0 with the selected difficulty from _newGameForm)
            _board = new BoardModel(_gameDifficulty);

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
                    //button.Click += BtnSquareClickEH;

                    // Store the location of the button in
                    // the Tag property using a Point object
                    button.Tag = new Point(row, col);
                    // Add the button to the panel's controls
                    pnlMinesweeperBoard.Controls.Add(_buttons[row, col]);
                }
            }

        } // End of SetUpButtons method
    }
}
