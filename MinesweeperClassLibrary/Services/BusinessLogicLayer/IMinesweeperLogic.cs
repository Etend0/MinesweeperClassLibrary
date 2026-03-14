using MinesweeperClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 3/8/2026
 * Minesweeper Class Library
 * Milestone 1
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Interface for Minesweeper game logic operations
    /// </summary>
    public interface IMinesweeperLogic
    {
        // Properties
        int RewardsRemaining { get; }
        int DifficultyLevels { get; }
        int Size { get; }
        CellModel[,] Cells { get; }

        // Methods
        /// <summary>
        /// Method to get the size of the board
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        int GetSize(int size);

        /// <summary>
        /// Method to setup the rewards on the board
        /// </summary>
        /// <param name="Cells"></param>
        /// <returns></returns>
        CellModel[,] SetupRewards(CellModel[,] Cells);

        /// <summary>
        /// Method to setup the bombs on the board
        /// </summary>
        /// <param name="Cells"></param>
        /// <returns></returns>
        CellModel[,] SetupBombs(CellModel[,] Cells);

        /// <summary>
        /// Method to count the number of bombs surrounding each cell
        /// </summary>
        void CountBombs();

        /// <summary>
        /// Method to print the board to the console
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        bool PrintAnswers(bool check);

        /// <summary>
        /// Method to update the cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="checkOrFlag"></param>
        void UpdateCell(int x, int y, int checkOrFlag);

        /// <summary>
        /// Determines if a cell at position is a bomb
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IdentifyCell(int x, int y);

        /// <summary>
        /// Decreases the current reward count by one
        /// </summary>
        void DecrementRewards();
    }
}
