using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

/*
 * Elijah Hodge
 * CST - 250
 * 06/29/2026
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

        // Variable to track if the player has flagged all bombs
        static bool flaggedAllBombs;

        static void Main(string[] args)
        {
            // Start the game
            Console.WriteLine("Hello, welcome to Minesweeper!");

            // Display the answer keys for the first board
            Console.WriteLine("Here is the answer key for the first board");
            // Create a new board with difficulty level 1
            BoardModel board = new BoardModel(1);
            // Create a new instance of the MinesweeperLogic class
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Get the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Set up the rewards on the board
            minesweeperLogic.SetupRewards(board.Cells);
            // Set up the bombs on the board
            minesweeperLogic.SetupBombs(board.Cells);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers();

            victory = false;

            death = false;

            while (!victory && !death)
            {
                // Keep looping until we either win or lose
                while (!victory && !death)
                {
                    // Initialize variables for the loop
                    int x = 0;

                    int y = 0;

                    int checkOrFlag = 0;

                    Console.WriteLine("Game in progress");

                    // Here is the current board
                    PrintBoard(board);

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

                    if (board.RewardsRemaining == 0)
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

                    // Check to see if the user tries to use a number other than 1 or 2 when we have no rewards
                    if (checkOrFlag > 2 && board.RewardsRemaining == 0)
                    {
                        // If so, prompt the user to enter 1 or 2 and skip the rest
                        Console.WriteLine("Please input 1 or 2");
                        continue;
                    }

                    CellModel currentCell = board.Cells[x, y];

                    // Check if the user tries to use 3 to use a reward when they have rewards available
                    if (checkOrFlag == 3 && board.RewardsRemaining > 0)
                    {
                        // Check if the cell is a bomb
                        bool checkBomb = currentCell.IsBomb;
                        // Print back results
                        Console.WriteLine("Is it a bomb? " + checkBomb);
                        // Take away reward
                        board.RewardsRemaining--;
                    }
                    else
                    {
                        if (checkOrFlag == 1)
                        {
                            if(currentCell.HasSpecialReward && !currentCell.IsVisited)
                            {
                                // If the cell has a special reward, increment the number of found rewards
                                board.RewardsRemaining++;

                                Console.WriteLine("You found a reward!");
                            }
                            // If the user wants to check the cell, set IsVisited to true
                            currentCell.IsVisited = true;
                        }
                        else if (checkOrFlag == 2)
                        {
                            // If the user wants to flag the cell, set IsFlagged to true
                            currentCell.IsFlagged = true;
                        }
                    }

                    (victory, death, flaggedAllBombs) = board.DetermineGameState();
                }
            }

            PrintBoard(board);

            if (victory && !death)
            {
                if (!flaggedAllBombs)
                {
                    // If the player has won, display a victory message
                    Console.WriteLine("Congratulations! You have won the game!");
                }
                else
                {
                    // If the player flagged all the bombs, display a special victory message
                    Console.WriteLine("Congratulations! You have won the game by flagging all bombs!");
                }
            }
            else
            {
                // If the player has lost, display a game over message
                Console.WriteLine("Game Over! You have lost the game!");
            }

            // Prompt the user to press any key to exit
            Console.WriteLine("Press any key to exit...");
        }

        /// <summary>
        /// Method to print the current state of the board to the console
        /// </summary>
        /// <param name="board"></param>
        public static void PrintBoard(BoardModel board)
        {
            int _size = board.Size;
            // Column headers
            Console.Write("   ");

            // Loop through the columns and print the column index as a header
            for (int k = 0; k < _size; k++)
            {
                Console.Write($" {k,2} ");
            }
            Console.WriteLine();

            // Top border
            Console.Write("   ");
            for (int k = 0; k < _size; k++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");

            // Rows
            for (int y = 0; y < _size; y++)
            {
                // Row index
                Console.Write($"{y,2} ");

                // Cells in row
                for (int x = 0; x < _size; x++)
                {
                    // Set the color for the cell based on its type and number of bomb neighbors
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"| ");

                    // Get the current cell and its type and number of bomb neighbors
                    CellModel currentCell = board.Cells[y, x];

                    // Check if the current cell has been visited
                    if (currentCell.IsVisited)
                    {
                        // Get the bool reward value to determine if the cell is a reward or not
                        bool reward = currentCell.HasSpecialReward;
                        // Get the bool bomb value to determine if the cell is a bomb or not
                        bool bomb = currentCell.IsBomb;
                        // Get the string representation of the cell to draw
                        string type = currentCell.ToString();
                        // Get the number of bomb neighbors for the current cell
                        int bombNeighbors = currentCell.NumberOfBombNeighbors;

                        // Set the color based on the number of bomb neighbors if the cell is not a bomb
                        if (bombNeighbors != 0 && bomb != true)
                        {
                            switch (bombNeighbors)
                            {
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;

                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;

                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;

                                case 4:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                            }
                        }

                        // If the cell has a reward, set the color to blue
                        if (reward)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        // If the cell is a bomb, set the color to red
                        if (bomb)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }

                        // Draw cell with vertical separator
                        Console.Write($"{type}");
                    }
                    else
                    {
                        if (!currentCell.IsFlagged)
                        {
                            // If the cell is not visited, draw a blank cell
                            Console.Write("? ");
                        }
                        else
                        {
                            // If the cell is flagged, draw its state (which should be flagged)
                            Console.Write(currentCell.ToString());
                        }
                    }
                }

                // Reset color and draw right border
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");

                // Row separator
                Console.Write("   ");
                for (int k = 0; k < _size; k++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }
        }
    }
}
