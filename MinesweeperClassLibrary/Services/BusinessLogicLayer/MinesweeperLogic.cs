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
        public int RewardsRemaining { get; set; }
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

        /// <summary>
        /// Public constructor for the MinesweeperLogic class that initializes the board size, rewards remaining, and difficulty levels
        /// </summary>
        /// <param name="Cells"></param>
        /// <param name="rateOfRewards"></param>
        /// <returns></returns>
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
                Row = random.Next(0, Size);
                Column = random.Next(0, Size);
                if (!(Cells[Row, Column] is RewardCellModel) && !(Cells[Row, Column] is BombCellModel))
                {
                    Cells[Row, Column] = new RewardCellModel(Row, Column);
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
                Row = random.Next(0, Size);
                Column = random.Next(0, Size);
                if (!(Cells[Row, Column] is BombCellModel) && !(Cells[Row, Column] is RewardCellModel))
                {
                    Cells[Row, Column] = new BombCellModel(Row, Column);
                    bombsPlaced++;
                }
            }
            // Return the updated cells with bombs placed
            return Cells;
        }

        /// <summary>
        /// Method to count the number of bombs surrounding each cell and update the NumberOfBombNeighbors property of each cell
        /// </summary>
        public void CountBombs()
        {
            // Loop through each cell in the grid
            for (int y = 0; y < Size; y++)
            {
                // Loop through each cell in the row
                for (int x = 0; x < Size; x++)
                {
                    // Get the current cell and its type and number of bomb neighbors
                    CellModel currentCell = Cells[y, x];
                    // Initialize a count variable to keep track of the number of bombs found around the current cell
                    int count = 0;
                    // If the current cell is not a bomb, count the number of bombs around it and update the NumberOfBombNeighbors property of the cell
                    if (!(currentCell is BombCellModel))
                    {
                        // Set our bombs found variable to 0 before we start counting
                        int bombsFound = 0;
                        // Loop through the 8 neighboring cells around the current cell
                        while (count < 8)
                        {
                            // Set the neighbor row
                            int neighborRow = y;
                            // Set the neighbor column
                            int neighborCol = x;
                            // Use a switch statement to determine the position of the neighboring cell based on the count variable
                            switch (count)
                            {
                                case 0: neighborRow = y - 1; neighborCol = x; break;
                                case 1: neighborRow = y - 1; neighborCol = x + 1; break;
                                case 2: neighborRow = y; neighborCol = x + 1; break;
                                case 3: neighborRow = y + 1; neighborCol = x + 1; break;
                                case 4: neighborRow = y + 1; neighborCol = x; break;
                                case 5: neighborRow = y + 1; neighborCol = x - 1; break;
                                case 6: neighborRow = y; neighborCol = x - 1; break;
                                case 7: neighborRow = y - 1; neighborCol = x - 1; break;
                            }
                            // Check if the neighboring cell is within the bounds of the board and if it is a bomb, increment the bombs found variable
                            if (neighborRow >= 0 && neighborRow < Size && neighborCol >= 0 && neighborCol < Size)
                            {
                                if (Cells[neighborRow, neighborCol] is BombCellModel)
                                {
                                    bombsFound++;
                                }
                            }
                            // Increment the count variable to move to the next neighboring cell
                            count++;
                        }
                        // After counting the number of bombs around the current cell, update the NumberOfBombNeighbors property of the cell with the count of bombs found
                        currentCell.SetNumberOfBombNeighbors(bombsFound);
                    }
                    else
                    {
                        // If the current cell is a bomb, set the NumberOfBombNeighbors property to a special value to indicate that it is a bomb
                        currentCell.SetNumberOfBombNeighbors(9);
                    }
                }
            }
        }

        /// <summary>
        /// Method to print the board to the console
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
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
                    // Use subclass checks for reward and bomb
                    bool reward = currentCell is RewardCellModel;
                    bool bomb = currentCell is BombCellModel;
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

                    // Set the color for the cell based on its type and number of bomb neighbors, but only if we're checking the board or the cell has been visited (old)
                    if (check || currentCell.isVisited)
                    {
                        if (bombNeighbors != 0 && !bomb && !reward)
                        {
                            switch (bombNeighbors)
                            {
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                            }
                        }
                        if (reward)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
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

            // If the player is checking the cell, reveal it and if it's a reward, increase the rewards remaining by 1, otherwise if it's not a bomb or reward, call the FloodFill method to reveal neighboring cells
            if (checkOrFlag == 1)
            {
                // Check if the cell is a bom, reward or has bomb neighbors
                if (currentCell is BombCellModel || currentCell is RewardCellModel || currentCell.NumberOfBombNeighbors > 0)
                {
                    // Reveal the cell
                    currentCell.SetVisited(true);
                    // If the cell is a reward, increase the rewards remaining by 1 and print a message to the console
                    if (currentCell is RewardCellModel)
                    {
                        // Increase the rewards remaining by 1
                        RewardsRemaining++;
                        Console.WriteLine("You found a reward!");
                    }
                }
                else
                {
                    // If the cell is not a bomb, reward, or has bomb neighbors, call the
                    FloodFill(board, currentCell.Row, currentCell.Column);
                }
            }
            else if (checkOrFlag == 2)
            {
                currentCell.SetFlagged(true);
            }
            else if (checkOrFlag == 0)
            {
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
            CellModel currentCell = Cells[y, x];
            return currentCell is BombCellModel;
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
        /// Resets the number of rewards to 0
        /// </summary>
        public void ResetRewards()
        {
            // Reset rewards to 0
            RewardsRemaining = 0;
        }

        /// <summary>
        /// Get the board model from program
        /// </summary>
        /// <param name="board"></param>
        public void GetBoard(BoardModel board)
        {
            // Set the board property to the given board and set the Cells property to the board's cells
            this.board = board;
            // Set the Cells property to the board's cells
            this.Cells = board.Cells;
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
            if (currentCell is BombCellModel || currentCell.isVisited || currentCell is RewardCellModel)
            {
                return board;
            }

            // Clear the flag if the cell was flagged
            if (currentCell.isFlagged)
            {
                // Unflag the cell
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