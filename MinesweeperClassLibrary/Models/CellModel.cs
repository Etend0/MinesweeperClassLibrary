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
    public class CellModel
    {
        // Class level properties
        public int Row { get; set; }
        public int Column { get; set; }
        public String Type { get; set; }
        public bool isVisited { get; set; }
        public bool isBomb { get; set; }
        public bool isFlagged { get; set; }
        public int NumberOfBombNeighbors { get; set; }
        public bool HasSpecialReward { get; set; }

        /// <summary>
        /// Default constructor for the cell model
        /// </summary>
        public CellModel(int row, int column, String type, bool isVisited, bool isBomb, bool isFlagged, int numberOfBombNeighbors, bool hasSpecialReward)
        {
            Row = row;
            Column = column;
            Type = type;
            this.isVisited = isVisited;
            this.isBomb = isBomb;
            this.isFlagged = isFlagged;
            NumberOfBombNeighbors = numberOfBombNeighbors;
            HasSpecialReward = hasSpecialReward;
        }

        /// <summary>
        /// Print cell to console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Row} {Column} {Type} {isVisited} {isBomb} {isFlagged} {NumberOfBombNeighbors} {HasSpecialReward}";
        }

        /// <summary>
        /// Return the type of cell
        /// </summary>
        public String DrawMe()
        {
            // If the type is B, we know it's a bomb
            if (isBomb)
            {
                Type = "B ";
            }
            else
            {
                // If NumberOfBombNeighbors is 0, we have no bombs around and should print an empty dot
                if (NumberOfBombNeighbors == 0)
                {
                    Type = ". ";
                }
                else
                {
                    // If NumberOfBombNeighors is not 0, print the number of bombs surrounding
                    Type = NumberOfBombNeighbors + " ";
                }
            }

            // Return the type of cell
            return Type;
        }
    }
}