using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

/*
 * Elijah Hodge
 * CST - 250
 * 3/8/2026
 * Minesweeper Class Library
 * Milestone 1
 */

namespace MinesweeperConsoleApp
{
    internal class Program
    {
        // Declare static variables for game state

        // Variable to track if the player has won the game
        static bool victory;

        // Variable to track if the player has lost the game
        static bool death;

        // Variable to track the current state of the game (still playing, won, or lost)
        static String state;

        // Create an instance of the MinesweeperLogic
        static IMinesweeperLogic minesweeperLogic = new MinesweeperLogic();

        // Create a new board with difficulty level 1
        static BoardModel board = new BoardModel(3);

        static void Main(string[] args)
        {
            // Set victory to false
            victory = false;

            // Set death to false
            death = false;

            // Set state to empty
            state = " ";

            // Start the game
            Console.WriteLine("Hello, welcome to Minesweeper!");

            // Display the answer keys for the first board
            Console.WriteLine("Here is the answer key for the first board");
            // Set the board model for the MinesweeperLogic
            minesweeperLogic.GetBoard(board);
            // Get the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Set up the rewards on the board
            minesweeperLogic.SetupRewards(board.Cells, 0.02);
            // Set up the bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();
            // Show the answers
            ShowAnswers();

            // Keep looping until we either win or lose
            while (!victory && !death)
            {
                // Initialize variables for the loop
                int x = 0;

                int y = 0;

                int checkOrFlag = 0;

                // Here is the current board
                PrintBoard();

                // Break the loop if we have either won or lost
                if (victory || death)
                {
                    // Don't continue loop
                    break;
                }

                // Prompt the user for the row
                Console.WriteLine("Enter the row number");

                // Read the user's input for the row
                string inputRow = Console.ReadLine();

                // Try to parse the user's input for the row, if it's not a valid integer, prompt them again
                if (int.TryParse(inputRow, out int row))
                {
                    // Grab the row number from the user's input
                    x = row;

                    // Check if the user's input for the row and column is within the bounds of the board
                    if (x < 0 || x >= board.Size)
                    {
                        // If the user's input for the row or column is out of bounds, display an error message and prompt them again
                        Console.WriteLine("Invalid row number. Please enter values between 0 and " + (board.Size - 1));
                        // Skip the rest of the loop
                        continue;
                    }
                }
                else
                {
                    // If the user's input is not a valid integer, display an error message and prompt them again
                    Console.WriteLine("Invalid input for row. Please enter a valid integer.");
                    // Skip the rest of the loop
                    continue;
                }

                // Prompt the user to enter a cell to reveal
                Console.WriteLine("Enter the column number");

                // Read the user's input for the column
                string inputColumn = Console.ReadLine();

                // Try to parse the user's input for the column, if it's not a valid integer, prompt them again
                if (int.TryParse(inputColumn, out int column))
                {
                    // Grab the column number from the user's input
                    y = column;

                    // Check if the user's input for the row and column is within the bounds of the board
                    if (y < 0 || y >= board.Size)
                    {
                        // If the user's input for the row or column is out of bounds, display an error message and prompt them again
                        Console.WriteLine("Invalid column number. Please enter values between 0 and " + (board.Size - 1));
                        // Skip the rest of the loop
                        continue;
                    }
                }
                else
                {
                    // If the user's input is not a valid integer, display an error message and prompt them again
                    Console.WriteLine("Invalid input for column. Please enter a valid integer.");
                    // Skip the rest of the loop
                    continue;
                }

                if (minesweeperLogic.RewardsRemaining == 0)
                {
                    // Prompt the user to enter a cell to reveal
                    Console.WriteLine("Enter 1 to visit, enter 2 to flag");
                }
                else
                {
                    // Prompt the user to enter a cell to reveal
                    Console.WriteLine("Enter 1 to visit, enter 2 to flag, 3 to use reward (cell viewer)");
                }

                // Read the user's input for whether they want to check or flag the cell or use a reward if they have one available
                string input = Console.ReadLine();

                // Parse the input to check if it's a valid integer
                if (int.TryParse(input, out int flag))
                {
                    // Grab the user's input for whether they want to check or flag the cell
                    checkOrFlag = flag;
                }
                else
                {
                    // If the user's input is not a valid integer, display an error message and prompt them again
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    // Skip the rest of the loop
                    continue;
                }

                // Check to see if the user tries to use a number other than 1 or 2 when we have to rewards
                if (checkOrFlag > 2 && minesweeperLogic.RewardsRemaining == 0)
                {
                    // If so, prompt the user to enter 1 or 2 and skip the rest
                    Console.WriteLine("Please input 1 or 2");
                    continue;
                }

                // Check if the user tries to use 3 to use a reward when they have rewards available
                if (checkOrFlag == 3 && minesweeperLogic.RewardsRemaining > 0)
                {
                    // Check if the cell is a bomb
                    bool checkBomb = minesweeperLogic.IdentifyCell(x, y);
                    // Print back results
                    Console.WriteLine("Is it a bomb? " + checkBomb);
                    // Take away reward
                    minesweeperLogic.DecrementRewards();
                }
                else
                {
                    // Otherwise, update the cell based on the user's input for whether they want to check or flag the cell
                    minesweeperLogic.UpdateCell(x, y, checkOrFlag);
                }
            }

            // Prompt the user to press any key to exit on end of game
            Console.Write("Press anything to exit.");
        }

        // Method to print the board to the console
        static void PrintBoard()
        {
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers(false);

            // Determine the game state after the user's move
            state = board.DetermineGameState(state);

            // Check the game state for whether we have won, lost, or are still playing
            switch (state)
            {
                // If we are still playing, we don't need to do anything
                case "StillPlaying":
                    Console.WriteLine("Game in progress");
                    break;

                // If we have won, set victory to true
                case "Won":
                    // Set victory to true
                    victory = true;
                    // Write to the console of victory results
                    Console.WriteLine("Congratulations, you won!");
                    break;

                // If we have lost, set death to true
                case "Lost":
                    // Set death to true
                    death = true;
                    // Write to the console of death results
                    Console.WriteLine("* KABOOM! *");
                    Console.WriteLine("Sorry, you lost!");
                    break;
            }
        }

        // Method to show the answers for the board
        static void ShowAnswers()
        {
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers(true);
        }
    }
}
