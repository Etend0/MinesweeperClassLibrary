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
        public bool isFlagged { get; set; }
        public int NumberOfBombNeighbors { get; set; }

        /// <summary>
        /// Default constructor for JSON serialization
        /// </summary>
        public CellModel()
        {
            Row = 0;
            Column = 0;
            Type = " ";
            isVisited = false;
            isFlagged = false;
            NumberOfBombNeighbors = 0;
        }

        /// <summary>
        /// Parameterized constructor for the cell model
        /// </summary>
        public CellModel(int row, int column, String type, bool isVisited, bool isFlagged, int numberOfBombNeighbors)
        {
            Row = row;
            Column = column;
            Type = type;
            this.isVisited = isVisited;
            this.isFlagged = isFlagged;
            NumberOfBombNeighbors = numberOfBombNeighbors;
        }

        /// <summary>
        /// Sets the visited bool of the cell
        /// </summary>
        /// <param name="visited"></param>
        public void SetVisited(bool visited)
        {
            // Set the isVisited property to the value of visited
            isVisited = visited;
        }

        // Returns if the cell has been visited or not
        public bool IsVisited()
        {
            // Return the value of the isVisited property
            return isVisited;
        }

        // Sets the flagged bool of the cell
        public void SetFlagged(bool flagged)
        {
            // Set the isFlagged property to the value of flagged
            isFlagged = flagged;
        }

        // Sets the number of bombs surrounding the cell
        public void SetNumberOfBombNeighbors(int numberOfBombNeighbors)
        {
            // Set the NumberOfBombNeighbors property to the value of numberOfBombNeighbors
            NumberOfBombNeighbors = numberOfBombNeighbors;
        }

        /// <summary>
        /// Print cell to console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Row} {Column} {Type} {isVisited} {isFlagged} {NumberOfBombNeighbors}";
        }

        /// <summary>
        /// Return the type of cell
        /// </summary>
        public virtual String DrawMe()
        {
            if (isVisited)
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
            else
            {
                // If the cell if not flagged, we should print a question mark
                if (isFlagged == false)
                {
                    Type = "? ";
                }
                // Otherwise we should print an F
                else
                {
                    Type = "F ";
                }
            }
            return Type;
        }

        /// <summary>
        /// Return the type of cell
        /// </summary>
        public virtual String DrawMeCheat()
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
            return Type;
        }
    }
}