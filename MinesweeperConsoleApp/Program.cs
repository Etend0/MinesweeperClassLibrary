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
        public static bool Start;

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
            // Set up the bombs on the board
            minesweeperLogic.SetupBombs(board.Cells);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();
            // Print the answer key for the board
            minesweeperLogic.PrintAnswers();

            // Display the answer keys for the second board
            Console.WriteLine("Here is the answer key for the second board");
            // Create a new board with difficulty level 2
            BoardModel board2 = new BoardModel(2);
            // Create a new instance of the MinesweeperLogic class
            MinesweeperLogic minesweeperLogic2 = new MinesweeperLogic();
            // Get the size of the board
            minesweeperLogic2.GetSize(board2.Size);
            // Set up the bombs on the board
            minesweeperLogic2.SetupBombs(board2.Cells);
            // Count the bombs on the board
            minesweeperLogic2.CountBombs();
            // Print the answer key for the board
            minesweeperLogic2.PrintAnswers();

            // Prompt the user to press any key to exit
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
