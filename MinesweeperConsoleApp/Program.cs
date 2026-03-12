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

        static String state;

        // Create an instance of the MinesweeperLogic
        static MinesweeperLogic minesweeperLogic = new MinesweeperLogic();

        // Create a new board with difficulty level 1
        static BoardModel board = new BoardModel(0);

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
            // Get the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Set up the bombs on the board
            minesweeperLogic.SetupBombs(board.Cells);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();
            // Show the answers
            ShowAnswers();

            // Keep looping until we either win or lose
            while (!victory && !death)
            {
                int x = 0;

                int y = 0;

                int checkOrFlag = 0;

                // Here is the current board
                PrintBoard();

                // Prompt the user for the row
                Console.WriteLine("Enter the row number");
                string inputRow = Console.ReadLine();

                if (int.TryParse(inputRow, out int row))
                {
                    x = row;
                }
                else
                {
                    Console.WriteLine("Invalid input for row. Please enter a valid integer.");
                    continue; // Skip the rest of the loop and prompt again
                }

                // Prompt the user to enter a cell to reveal
                Console.WriteLine("Enter the column number");
                string inputColumn = Console.ReadLine();

                if (int.TryParse(inputColumn, out int column))
                {
                    y = column;
                }
                else
                {
                    Console.WriteLine("Invalid input for column. Please enter a valid integer.");
                    continue; // Skip the rest of the loop and prompt again
                }

                // Prompt the user to enter a cell to reveal
                Console.WriteLine("Enter 1 to visit, enter 2 to flag");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int flag))
                {
                    checkOrFlag = flag;
                }
                else
                {
                    Console.WriteLine("Invalid input for row number. Please enter a valid integer.");
                    continue; // Skip the rest of the loop and prompt again
                }

                if (x < 0 || x >= board.Size || y < 0 || y >= board.Size)
                {
                    Console.WriteLine("Invalid row or column number. Please enter values between 0 and " + (board.Size - 1));
                    continue; // Skip the rest of the loop and prompt again
                }
                else
                {
                    minesweeperLogic.UpdateCell(x, y, checkOrFlag);
                }
            }
        }

        static void PrintBoard()
        {
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers(false);

            state = board.DetermineGameState(state);

            switch(state)
            {
                case "StillPlaying":
                    break;

                case "Won":
                    victory = true;
                    Console.WriteLine("Congratulations, you won!");
                    break;

                case "Lost":
                    death = true;
                    Console.WriteLine("* KABOOM! *");
                    Console.WriteLine("Sorry, you lost!");
                    break;
            }
        }

        static void ShowAnswers()
        {
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers(true);
        }
    }
}
