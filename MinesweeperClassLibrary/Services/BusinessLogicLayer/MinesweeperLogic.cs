using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 06/29/2026
 * Minesweeper Class Library
 * Milestone 1
 */

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    public class MinesweeperLogic
    {
        // Declare class level variables
        private MinesweeperDAO _minesweeperDAO;

        /// <summary>
        /// Default constructor for MinesweeperLogic
        /// </summary>
        public MinesweeperLogic()
        {
            // Initialize the DAO variable
            _minesweeperDAO = new MinesweeperDAO();
        }

        /// <summary>
        /// Get the size of the board based on the selected difficulty
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public int GetSize(int size)
        {
            // Call the DAO method to get the size of the board
            return _minesweeperDAO.GetSize(size);
        }

        /// <summary>
        /// Get the set size of the board
        /// </summary>
        /// <returns></returns>
        public int ReturnSize()
        {
            // Call the DAO method to return the size of the board
            return _minesweeperDAO.ReturnSize();
        }

        /// <summary>
        /// Method to setup the bombs on the board
        /// </summary>
        /// <param name="Cells"></param>
        /// <returns></returns>
        public CellModel[,] SetupBombs(CellModel[,] Cells)
        {
            // Call the DAO method to setup the bombs on the board
            return _minesweeperDAO.SetupBombs(Cells);
        }

        /// <summary>
        /// Count the number of bombs surrounding the cells
        /// </summary>
        public void CountBombs()
        {
            // Call the DAO method to count the number of bombs surrounding the cells
            _minesweeperDAO.CountBombs();
        }

        /// <summary>
        /// Print the answers of the board to the console
        /// </summary>
        public void PrintAnswers()
        {
            // Call the DAO method to print the answers of the board to the console
            _minesweeperDAO.PrintAnswers();
        }
    }
}
