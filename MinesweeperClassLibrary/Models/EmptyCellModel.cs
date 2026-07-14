using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperClassLibrary.Models
{
    public class EmptyCellModel : CellModel
    {
        /// <summary>
        /// Default constructor for the bomb cell model
        /// </summary>
        public EmptyCellModel() : base()
        {
            // Set the type to ". "
            Type = ". ";
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
        public EmptyCellModel(int row, int column, string type, bool isVisited, bool isBomb, bool isFlagged, int numberOfBombNeighbors, bool hasSpecialReward) : base(row, column, type, isVisited, isBomb, isFlagged, numberOfBombNeighbors, hasSpecialReward)
        {
            // Set the type to ". "
            Type = ". ";
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
                if (NumberOfBombNeighbors > 0)
                {
                    // Return the string representation of the empty cell with the number of bomb neighbors
                    return NumberOfBombNeighbors + " ";
                }
                else
                {
                    // Return the string representation of this cell
                    return Type;
                }
            }
            else
            {
                // Return the string representation of the flagged cell
                return "F ";
            }
        }
    }
}
