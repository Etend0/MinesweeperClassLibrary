using MinesweeperClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer
{
    public class MinesweeperLogic : IMinesweeperLogic
    {
        // Class properties
        public int RewardsRemaining { get; private set; }
        public int DifficultyLevels { get; private set; }
        public int Size { get; private set; }
        public CellModel[,] Cells { get; private set; }
        public BoardModel board { get; private set; }

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
        /// Method to get the size of the board
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public int GetSize(int size)
        {
            Size = size;
            return Size;
        }

        public CellModel[,] SetupRewards(CellModel[,] Cells, double rateOfRewards)
        {
            // Grab the current list of cells and set it to the class property
            this.Cells = Cells;
            // Create a random number generator to randomize positions on the board
            Random random = new Random();
            // Calculate the total number of cells on the board
            int GridSize = Size * Size;
            // Calculate the number of rewards to place based on the given rateOfRewards
            double numberOfRewards = GridSize * rateOfRewards;
            // Randomly select a row position for the reward
            int Row = random.Next(0, Size);
            // Randomly select a column position for the reward
            int Column = random.Next(0, Size);
            // Convert the number of rewards to an integer
            int FinalNumberRewards = Convert.ToInt32(numberOfRewards);
            // Set the current number of placed rewards
            int rewardsPlaced = 0;
            // Loop until the max number of rewards have been placed
            while (rewardsPlaced < FinalNumberRewards)
            {
                // Randomly select a row position for the reward
                Row = random.Next(0, Size);
                // Randomly select a column position for the reward
                Column = random.Next(0, Size);
                // Check if the chosen position does not have a bomb or reward, if it doesn't, place one there and increment the rewards placed counter
                if (Cells[Row, Column].HasSpecialReward != true)
                {
                    // Create the reward
                    Cells[Row, Column] = new RewardCellModel(Row, Column);
                    // Increment the rewards placed counter
                    rewardsPlaced++;
                }
            }
            // Return the updated cells with rewards placed
            return Cells;
        }

        /// <summary>
        /// Method to setup the bombs on the board
        /// </summary>
        /// <param name="Cells"></param>
        /// <returns></returns>
        public CellModel[,] SetupBombs(CellModel[,] Cells, double rateOfBombs)
        {
            // Grab the current list of cells and set it to the class property
            this.Cells = Cells;

            // Create a random number generator to randomize positions on the board
            Random random = new Random();

            // Calculate the total number of cells on the board
            int GridSize = Size * Size;

            // Calculate the number of bombs to place based on rateOfBombs
            double NumberOfBombs = GridSize * rateOfBombs;

            // Randomly select a row position for the bomb
            int Row = random.Next(0, Size);

            // Randomly select a column position for the bomb
            int Column = random.Next(0, Size);

            // Convert the number of bombs to an integer
            int FinalNumberBombs = Convert.ToInt32(NumberOfBombs);

            // Set the current number of placed bombs
            int bombsPlaced = 0;

            // Loop until the max number of bombs have been placed
            while (bombsPlaced < FinalNumberBombs)
            {
                // Randomly select a row position for the bomb
                Row = random.Next(0, Size);

                // Randomly select a column position for the bomb
                Column = random.Next(0, Size);

                // Check if the chosen position does not have a bomb, if it doesn't, place one there and increment the bombs placed counter
                if (Cells[Row, Column].isBomb != true && Cells[Row, Column].HasSpecialReward != true)
                {
                    // Create the bomb
                    Cells[Row, Column] = new BombCellModel(Row, Column);

                    // Increment the bombs placed counter
                    bombsPlaced++;
                }
            }
            // Return the updated cells with bombs placed
            return Cells;
        }

        // Method to count the number of bombs surrounding each cell and update the NumberOfBombNeighbors property of each cell
        public void CountBombs()
        {
            // Loop through each cell in the grid
            for (int y = 0; y < Size; y++)
            {
                // Loop through each cell in the current row
                for (int x = 0; x < Size; x++)
                {
                    // Get the current cell
                    CellModel currentCell = Cells[y, x];

                    // Set the count to 0
                    int count = 0;

                    // If the current cell is not a bomb, check its neighbors for bombs and increment the count for each bomb found
                    if (currentCell.isBomb != true)
                    {
                        int bombsFound = 0;

                        // Check each corrospinding neighboring position around the cell in 8 directions
                        while (count < 8)
                        {
                            // Set the initial neighbor row and column to the current cell's position
                            int neighborRow = y;
                            int neighborCol = x;

                            // Update the neighbor row and column based on the current count to check each of the 8 neighboring positions
                            switch (count)
                            {
                                case 0: 
                                    // Top
                                    neighborRow = y - 1;
                                    neighborCol = x;
                                    break;
                                case 1: 
                                    // Right Diagonal Top
                                    neighborRow = y - 1;
                                    neighborCol = x + 1;
                                    break;
                                case 2: 
                                    // Right
                                    neighborRow = y;
                                    neighborCol = x + 1;
                                    break;
                                case 3: 
                                    // Right Diagonal Bottom
                                    neighborRow = y + 1;
                                    neighborCol = x + 1;
                                    break;
                                case 4: 
                                    // Bottom
                                    neighborRow = y + 1;
                                    neighborCol = x;
                                    break;
                                case 5: 
                                    // Left Diagonal Bottom
                                    neighborRow = y + 1;
                                    neighborCol = x - 1;
                                    break;
                                case 6: 
                                    // Left
                                    neighborRow = y;
                                    neighborCol = x - 1;
                                    break;
                                case 7: 
                                    // Left Diagonal Top
                                    neighborRow = y - 1;
                                    neighborCol = x - 1;
                                    break;
                            }

                            // Check to see if we aren't checking out of bound of the grid
                            if (neighborRow >= 0 && neighborRow < Size && neighborCol >= 0 && neighborCol < Size)
                            {
                                // If we aren't, check if we found a bomb, if so, increment the current cell's NumberOfBombNeighbors
                                if (Cells[neighborRow, neighborCol].isBomb == true)
                                {
                                    // Increment the current cell's NumberOfBombNeighbors
                                    bombsFound++;
                                }
                            }
                            // Increment our count to show we check that spot
                            count++;
                        }
                        currentCell.SetNumberOfBombNeighbors(bombsFound);
                    }
                    else
                    {
                        // If the current cell is a bomb, set its NumberOfBombNeighbors to 9 to indicate that it is a bomb and not a number cell
                        currentCell.SetNumberOfBombNeighbors(9);
                    }
                }
            }
        }

        // Method to print the board to the console
        public bool PrintAnswers(bool check)
        {
            // Column headers
            Console.Write("   ");

            // Loop through the columns and print the column index as a header
            for (int k = 0; k < Size; k++)
            {
                Console.Write($" {k,2} ");
            }
            Console.WriteLine();

            // Top border
            Console.Write("   ");
            for (int k = 0; k < Size; k++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");

            // Rows
            for (int y = 0; y < Size; y++)
            {
                // Row index
                Console.Write($"{y,2} ");

                // Cells in row
                for (int x = 0; x < Size; x++)
                {
                    // Set the color for the cell based on its type and number of bomb neighbors
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"| ");

                    // Get the current cell and its type and number of bomb neighbors
                    CellModel currentCell = Cells[y, x];
                    // Get the bool reward value to determine if the cell is a reward or not
                    bool reward = currentCell.HasSpecialReward;
                    // Get the bool bomb value to determine if the cell is a bomb or not
                    bool bomb = currentCell.isBomb;
                    // Get the string representation of the cell to draw
                    string type;

                    // Check if we're not viewing the board
                    if (check != true)
                    {
                        // If not, draw the cell as a hidden cell
                        type = currentCell.DrawMe();
                    }
                    else
                    {
                        // If we are, draw the cell as its true type
                        type = currentCell.DrawMeCheat();
                    }
                    // Get the number of bomb neighbors for the current cell
                    int bombNeighbors = currentCell.NumberOfBombNeighbors;

                    if (check || currentCell.isVisited)
                    {
                        // Set the color based on the number of bomb neighbors if the cell is not a bomb
                        if (bombNeighbors != 0 && bomb != true && reward != true)
                        {
                            switch (bombNeighbors)
                            {
                                case 1:
                                    // If the cell has 1 bomb neighbor, set the color to cyan
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;

                                case 2:
                                    // If the cell has 2 bomb neighbors, set the color to green
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;

                                case 3:
                                    // If the cell has 3 bomb neighbors, set the color to magenta
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;

                                case 4:
                                    // If the cell has 4 bomb neighbors, set the color to yellow
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                            }
                        }

                        // If the cell has a reward, set the color to blue
                        if (reward)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        // If the cell is a bomb, set the color to red
                        if (bomb)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }

                    // Draw cell with vertical separator
                    Console.Write($"{type}");
                }

                // Reset color and draw right border
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");

                // Row separator
                Console.Write("   ");
                for (int k = 0; k < Size; k++)
                {
                    Console.Write("+---");
                }
                Console.WriteLine("+");
            }

            return check;
        }

        /// <summary>
        /// Method to update the cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="checkOrFlag"></param>
        /// <returns></returns>
        public void UpdateCell(int x, int y, int checkOrFlag)
        {
            // Get the current cell and its type and number of bomb neighbors
            CellModel currentCell = Cells[y, x];

            // Check if we're flagging the cell
            if (checkOrFlag == 1)
            {
                if (currentCell.isBomb == true || currentCell.HasSpecialReward == true || currentCell.NumberOfBombNeighbors > 0)
                {
                    // If not, show the cell
                    currentCell.SetVisited(true);

                    // Check if the cell has a special reward
                    if (currentCell.HasSpecialReward == true)
                    {
                        // Increment RewardsRemaining
                        RewardsRemaining++;
                        // If yes, show the reward message
                        Console.WriteLine("You found a reward!");
                    }
                }
                else
                {
                    FloodFill(board, currentCell.Row, currentCell.Column);
                }
            }
            else if (checkOrFlag == 2)
            {
                // If yes, flag the cell
                currentCell.SetFlagged(true);
            }
            else if (checkOrFlag == 0)
            {
                // Unflag the cell
                currentCell.SetFlagged(false);
            }
        }

        /// <summary>
        /// Determines if a cell at position is a bomb
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IdentifyCell(int x, int y)
        {
            // Get the current cell
            CellModel currentCell = Cells[y, x];
            // Return if the cell is a bomb
            return currentCell.isBomb;
        }

        /// <summary>
        /// Decreases the number of rewards by 1
        /// </summary>
        public void DecrementRewards()
        {
            // Decrease rewards
            RewardsRemaining--;
        }

        /// <summary>
        /// Get the board model from program
        /// </summary>
        /// <param name="board"></param>
        public void GetBoard(BoardModel board)
        {
            this.board = board;
        }

        /// <summary>
        /// Fill the board with an algorithm on the given row and col
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public BoardModel FloodFill(BoardModel board, int row, int col)
        {
            // Change the text color to white
            Console.ForegroundColor = ConsoleColor.White;

            // Check if the cell is on the board
            if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
            {
                // If the cell is not on the board, end the method
                return board;
            }

            // Get the current cell and its type and number of bomb neighbors
            CellModel currentCell = Cells[row, col];

            // If the cell is a bomb, already visited, or a reward, end the method
            if (currentCell.isBomb || currentCell.isVisited || currentCell.HasSpecialReward)
            {
                return board;
            }

            // Clear the flag if the cell was flagged
            if (currentCell.isFlagged)
            {
                currentCell.SetFlagged(false);
            }

            // Reveal the cell
            currentCell.SetVisited(true);

            // If the cell has bombs around it, don't go through it
            if (currentCell.NumberOfBombNeighbors > 0)
            {
                return board;
            }

            // Call the FloodFill method to the east
            board = FloodFill(board, row, col + 1);

            // Call the FloodFill method to the north
            board = FloodFill(board, row - 1, col);

            // Call the FloodFill method to the south
            board = FloodFill(board, row + 1, col);

            // Call the FloodFill method to the west
            board = FloodFill(board, row, col - 1);

            // Call the FloodFill method to the north-east
            board = FloodFill(board, row - 1, col + 1);

            // Call the FloodFill method to the north-west
            board = FloodFill(board, row - 1, col - 1);

            // Call the FloodFill method to the south-east
            board = FloodFill(board, row + 1, col + 1);

            // Call the FloodFill method to the south-west
            board = FloodFill(board, row + 1, col - 1);

            // Return the board
            return board;
        }
    }
}