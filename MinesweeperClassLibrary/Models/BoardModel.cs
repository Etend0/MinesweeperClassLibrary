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
        /// Default constructor for JSON serialization
        /// </summary>
        public BoardModel()
        {
            StartTime = 0;
            EndTime = 0;
            Size = 0;
            Cells = null;
            DifficultyLevels = 0;
        }

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
                // Very Easy
                case 0:
                    Size = 6;
                    break;
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
                        Cells[i, j] = new CellModel(i, j, " ", false, false, false, 0, false);
                    }
                }
            }
        }

        /// <summary>
        /// Method to check the status of the board
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public String DetermineGameState(String state)
        {
            // Variable to track if a bomb has been detonated
            bool bombDetonated = false;

            // Variable to count the number of cells that have not been visited
            int cellCount = 0;

            // Variable to hold the current game state
            String gameState = " ";

            // Check if a bomb has been detonated, if not, loop through the grid to check for unvisited cells
            if (!bombDetonated)
            {
                // Loop through each cell in the grid
                for (int y = 0; y < Size; y++)
                {
                    // Loop through each cell in the current row
                    for (int x = 0; x < Size; x++)
                    {
                        // Get the current cell
                        CellModel currentCell = Cells[y, x];

                        // If the current cell is not a bomb, check its neighbors for bombs and increment the count for each bomb found
                        if (currentCell.isBomb != true)
                        {
                            // If the current cell has not been visited, increment the cell count
                            if (currentCell.isVisited != true)
                            {
                                // Increment the cell count
                                cellCount++;
                            }
                        }
                        else
                        {
                            // If the current cell is a bomb and has been visited, set the bomb detonated variable to true
                            if (currentCell.isVisited == true)
                            {
                                // Set the bomb detonated variable to true
                                bombDetonated = true;
                            }
                        }
                    }
                }
            }

            // If a bomb has not been detonated, continue the check
            if (!bombDetonated)
            {
                // If there are still unvisited cells, the game is still in progress
                if (cellCount != 0)
                {
                    // Set the game state to still playing
                    state = "StillPlaying";

                }
                else
                {
                    // If there are no unvisited cells, the game has been won
                    state = "Won";
                }
            }
            else
            {
                // If a bomb has been detonated, the game has been lost
                state = "Lost"; 
            }

            // Return the current game state
            return state;
        }
    }
}
