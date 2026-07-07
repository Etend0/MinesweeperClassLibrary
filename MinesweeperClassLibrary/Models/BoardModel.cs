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
    public class BoardModel
    {
        // Class level properties
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Size { get; set; }
        public CellModel[,] Cells { get; set; }
        public int DifficultyLevels { get; set; }
        public int RewardsRemaining { get; set; }

        /// <summary>
        ///  Set the current game state
        /// </summary>
        public enum GameState
        {
            InProgress,
            Won,
            Lost
        }

        /// <summary>
        /// Parameterized constructor for the board model class
        /// </summary>
        public BoardModel(int Difficulty)
        {
            // Get the selected difficulty
            DifficultyLevels = Difficulty;

            // Set the grid size based on the selected difficulty
            switch (DifficultyLevels)
            {
                // Very Easy
                case 0:
                    Size = 4;
                    break;
                // Easy
                case 1:
                    Size = 10;
                    break;
                // Medium
                case 2:
                    Size = 15;
                    break;
                // Hard
                case 3:
                    Size = 20;
                    break;
            }

            // Initialize the cells array based on the selected grid size
            Cells = new CellModel[Size, Size];

            // Populate the cells array with default cell models

            // Go through each column
            for (int i = 0; i < Size; i++)
            {
                // Go through each row
                for (int j = 0; j < Size; j++)
                {
                    // If the cell is null, create a new cell model with default values
                    if (Cells[i, j] == null)
                    {
                        // Create the default cell
                        Cells[i, j] = new EmptyCellModel(i, j, "0", false, false, false, 0, false);
                    }
                }
            }
        }

        /// <summary>
        /// Method to determine the current game state based on the board's cells
        /// </summary>
        public (bool IsGameOver, bool IsPlayerDead, bool FlaggedAllBombs) DetermineGameState()
        {
            // Set the current game state to InProgress
            GameState currentState = GameState.InProgress;

            // Keep track of the number of bombs on the board
            int bombCount = 0;

            // Keep track of the number of bombs that have been flagged
            int bombsFlagged = 0;

            // Keep track of the number of non-bomb cells that have been visited
            int cellsVisited = 0;

            // Get the board size
            int boardSize = Size * Size;

            // Count the number of bombs on the board
            for (int i = 0; i < Size; i++)
            {
                // Go through each row
                for (int j = 0; j < Size; j++)
                {
                    // Grab the current cell model
                    CellModel cellModel = Cells[i, j];

                    // Check if the cell is a bomb and has been flagged
                    if (cellModel.IsBomb)
                    {
                        // Increment the number of bombs on the board
                        bombCount++;
                    }
                }
            }

            // Check if the game is still in progress
            if (currentState == GameState.InProgress)
            {
                // Check if all non-bomb cells have been visited
                if (bombsFlagged < bombCount)
                {
                    // Go through each column
                    for (int i = 0; i < Size; i++)
                    {
                        // Go through each row
                        for (int j = 0; j < Size; j++)
                        {
                            // Grab the current cell model
                            CellModel cellModel = Cells[i, j];

                            // Check if the cell is a bomb and has been flagged
                            if (cellModel.IsBomb)
                            {
                                // Check if the bomb has been visited
                                if (cellModel.IsVisited)
                                {
                                    // If a bomb has been visited, the player has lost the game
                                    currentState = GameState.Lost;
                                    return (true, true, false);
                                }

                                // If not, check if it's been flagged
                                if (cellModel.IsFlagged)
                                {
                                    // If it has, increment the number of bombs that have been flagged
                                    bombsFlagged++;
                                }
                            }

                            // Check if the cell is not a bomb and has been visited
                            if (!cellModel.IsBomb && cellModel.IsVisited)
                            {
                                // Keep track of the number of non-bomb cells that have been visited
                                cellsVisited++;
                            }
                        }
                    }
                }

                if (bombsFlagged == bombCount)
                {
                    // If all bombs have been flagged, the player has won the game
                    currentState = GameState.Won;
                    return (true, false, true);
                }

                // If all non-bomb cells have been visited, the player has won the game
                if (cellsVisited == (boardSize - bombCount))
                {
                    // Set the current game state to Won and return true for IsGameOver and false for IsPlayerDead
                    currentState = GameState.Won;
                    return (true, false, false);
                }
            }

            // The game has not ended, return false for both IsGameOver and IsPlayerDead
            return (false, false, false);
        }
    }
}
