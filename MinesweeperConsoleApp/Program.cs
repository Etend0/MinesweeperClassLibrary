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
            Start = true;

            while (Start)
            {
                Console.WriteLine("Hello, welcome to Minesweeper!");

                Console.WriteLine("Here is the answer key for the first board");
                BoardModel board = new BoardModel(1);
                MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
                minesweeperLogic.GetSize(board.Size);
                minesweeperLogic.SetupBombs(board.Cells);
                minesweeperLogic.CountBombs();
                minesweeperLogic.PrintAnswers();

                Console.WriteLine("Here is the answer key for the second board");
                BoardModel board2 = new BoardModel(2);
                MinesweeperLogic minesweeperLogic2 = new MinesweeperLogic();
                minesweeperLogic2.GetSize(board2.Size);
                minesweeperLogic2.SetupBombs(board2.Cells);
                minesweeperLogic2.CountBombs();
                minesweeperLogic2.PrintAnswers();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Start = false;
            }
        }
    }
}
