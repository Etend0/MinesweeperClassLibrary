using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 06/28/2026
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
        public String Type { get; set; }
        public bool IsVisited { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public int NumberOfBombNeighbors { get; set; }
        public bool HasSpecialReward { get; set; }

        /// <summary>
        /// Default constructor for the cell model
        /// </summary>
        public CellModel()
        {
            Row = -1;
            Column = -1;
            Type = ". ";
            IsVisited = false;
            IsBomb = false;
            IsFlagged = false;
            NumberOfBombNeighbors = 0;
            HasSpecialReward = false;
        }

        /// <summary>
        /// Parameterized constructor for the cell model
        /// </summary>
        public CellModel(int row, int column, String type, bool isVisited, bool isBomb, bool isFlagged, int numberOfBombNeighbors, bool hasSpecialReward)
        {
            Row = row;
            Column = column;
            Type = type;
            this.IsVisited = isVisited;
            this.IsBomb = isBomb;
            this.IsFlagged = isFlagged;
            NumberOfBombNeighbors = numberOfBombNeighbors;
            HasSpecialReward = hasSpecialReward;
        }

        /// <summary>
        /// Print cell to console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Type}";
        }
    }
}