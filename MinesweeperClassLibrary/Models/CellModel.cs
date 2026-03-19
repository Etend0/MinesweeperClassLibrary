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
        public int Row { get; private set; }
        public int Column { get; private set; }
        public String Type { get; protected set; }
        public bool isVisited { get; private set; }
        public bool isBomb { get; private set; }
        public bool isFlagged { get; private set; }
        public int NumberOfBombNeighbors { get; private set; }
        public bool HasSpecialReward { get; private set; }

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
        /// Sets the visited bool of the cell
        /// </summary>
        /// <param name="visited"></param>
        public void SetVisited(bool visited)
        {
            // Set the isVisited property to the value of visited
            isVisited = visited;
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

        // Sets the has special reward bool of the cell
        public void SetHasSpecialReward(bool hasSpecialReward)
        {
            //  Set the HasSpecialReward property to the value of hasSpecialReward
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
        public virtual String DrawMe()
        {
            if (isVisited)
            {
                // If the type is B, we know it's a bomb
                if (isBomb)
                {
                    Type = "B ";
                }
                else if (HasSpecialReward)
                {
                    // If the cell is flagged, we should print an F
                    Type = "R ";
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
            }
            else
            {
                if (isFlagged == false)
                {
                    Type = "? ";
                }
                else
                {
                    Type = "F ";
                }
            }

            // Return the type of cell
            return Type;
        }

        /// <summary>
        /// Return the type of cell
        /// </summary>
        public virtual String DrawMeCheat()
        {
            // If the type is B, we know it's a bomb
            if (isBomb)
            {
                Type = "B ";
            }
            else if (HasSpecialReward)
            {
                // If the cell is flagged, we should print an F
                Type = "R ";
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