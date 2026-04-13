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
    public class BombCellModel : CellModel
    {
        /// <summary>
        /// Parameterless constructor for deserialization
        /// </summary>
        public BombCellModel() : base(0, 0, " ", false, false, 0) { }

        /// <summary>
        /// Parameterized constructor for the bomb cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public BombCellModel(int row, int column)
            : base(row, column, " ", false, false, 0)
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