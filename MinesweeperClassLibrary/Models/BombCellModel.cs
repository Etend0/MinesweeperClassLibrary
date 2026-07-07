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

namespace MinesweeperClassLibrary.Models
{
    internal class BombCellModel : CellModel
    {
        /// <summary>
        /// Default constructor for the bomb cell model
        /// </summary>
        public BombCellModel() : base()
        {
            // Set the bomb property to true
            IsBomb = true;
            // Set the number of bomb neighbors to 9
            NumberOfBombNeighbors = 9;
            // Set the type to "B "
            Type = "B ";
        }

        /// <summary>
        /// Parameterized constructor for the bomb cell model
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="type"></param>
        /// <param name="isVisited"></param>
        /// <param name="isBomb"></param>
        /// <param name="isFlagged"></param>
        /// <param name="numberOfBombNeighbors"></param>
        /// <param name="hasSpecialReward"></param>
        public BombCellModel(int row, int column, string type, bool isVisited, bool isBomb, bool isFlagged, int numberOfBombNeighbors, bool hasSpecialReward) : base(row, column, type, isVisited, isBomb, isFlagged, numberOfBombNeighbors, hasSpecialReward)
        {
            // Set the bomb property to true
            IsBomb = true;
            // Set the number of bomb neighbors to 9
            NumberOfBombNeighbors = 9;
            // Set the type to "B "
            Type = "B ";
        }

        /// <summary>
        /// ToString method for the bomb cell model
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Check if the cell is flagged
            if (!IsFlagged)
            {
                // Return the string representation of this cell
                return Type;
            }
            else
            {
                // Return the string representation of the flagged cell
                return "F ";
            }
        }
    }
}
