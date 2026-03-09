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

namespace MinesweeperClassLibrary.Models
{
    public class BoardModel
    {
        // Class level properties
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int Size { get; set; }
        public CellModel[,] Cells { get; set; }
        public int DifficultyLevels { get; set; }

        /// <summary>
        /// Parameterized constructor for the board model class
        /// </summary>
        public BoardModel(int Difficulty)
        {
            // Get the selected difficulty
            DifficultyLevels = Difficulty;

            // Set the grid size based on the selected difficulty
            switch (DifficultyLevels)
            {
                // Easy
                case 1:
                    Size = 10;
                    break;
                // Medium
                case 2:
                    Size = 15;
                    break;
                // Hard
                case 3:
                    Size = 20;
                    break;
            }

            // Initialize the cells array based on the selected grid size
            Cells = new CellModel[Size, Size];

            // Populate the cells array with default cell models

            // Go through each column
            for (int i = 0; i < Size; i++)
            {
                // Go through each row
                for (int j = 0; j < Size; j++)
                {
                    // If the cell is null, create a new cell model with default values
                    if (Cells[i, j] == null)
                    {
                        // Create the default cell
                        Cells[i, j] = new CellModel(i, j, "0", false, false, false, 0, false);
                    }
                }
            }
        }
    }
}
