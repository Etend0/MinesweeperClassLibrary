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
    internal class RewardCellModel : CellModel
    {
        /// <summary>
        /// Default constructor for the bomb cell model
        /// </summary>
        public RewardCellModel() : base()
        {
            // Set the special reward property to true
            HasSpecialReward = true;
            // Set the type to "R "
            Type = "r ";
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
        public RewardCellModel(int row, int column, string type, bool isVisited, bool isBomb, bool isFlagged, int numberOfBombNeighbors, bool hasSpecialReward) : base(row, column, type, isVisited, isBomb, isFlagged, numberOfBombNeighbors, hasSpecialReward)
        {
            // Set the special reward property to true
            HasSpecialReward = true;
            // Set the type to "R "
            Type = "r ";
        }

        /// <summary>
        /// ToString method for the reward cell model
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Check if the cell is flagged
            if (!IsFlagged)
            {
                // Return the string representation of the empty cell
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
