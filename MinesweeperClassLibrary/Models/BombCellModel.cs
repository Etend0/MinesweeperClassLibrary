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
    /// <summary>
    /// A cell that contains a bomb, inherits from CellModel
    /// </summary>
    public class BombCellModel : CellModel
    {
        /// <summary>
        /// Constructor for the bomb cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public BombCellModel(int row, int column)
            : base(row, column, " ", false, true, false, 0, false)
        {
        }

        /// <summary>
        /// Return the type of cell for a bomb
        /// </summary>
        public override string DrawMe()
        {
            if (isVisited)
            {
                // If the cell is visited, show the bomb
                Type = "B ";
            }
            else
            {
                // If the cell is not visited, show flagged or hidden
                Type = isFlagged ? "F " : "? ";
            }
            return Type;
        }

        /// <summary>
        /// Return the cheat type of cell for a bomb
        /// </summary>
        public override string DrawMeCheat()
        {
            // Always show the bomb on cheat view
            Type = "B ";
            return Type;
        }
    }
}