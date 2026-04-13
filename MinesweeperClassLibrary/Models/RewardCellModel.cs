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
    public class RewardCellModel : CellModel
    {
        /// <summary>
        /// Parameterless constructor for deserialization
        /// </summary>
        public RewardCellModel() : base(0, 0, " ", false, false, 0) { }

        /// <summary>
        /// Parameterized constructor for the reward cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public RewardCellModel(int row, int column)
            : base(row, column, " ", false, false, 0)
        {
        }

        /// <summary>
        /// Return the type of cell for a reward
        /// </summary>
        public override string DrawMe()
        {
            if (isVisited)
            {
                // If the cell is visited, show the reward
                Type = "R ";
            }
            else
            {
                // If the cell is not visited, show flagged or hidden
                Type = isFlagged ? "F " : "? ";
            }
            return Type;
        }

        /// <summary>
        /// Return the cheat type of cell for a reward
        /// </summary>
        public override string DrawMeCheat()
        {
            // Always show the reward on cheat view
            Type = "R ";
            return Type;
        }
    }
}