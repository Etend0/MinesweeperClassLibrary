using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;

/*
 * Elijah Hodge
 * CST - 250
 * 3/8/2026
 * Minesweeper Class Library
 * Milestone 1
 */

namespace MinesweeperClassLibrary.Tests
{
    public class MinesweeperLogicTests
    {
        // Test RewardsRemaining property get/set
        [Fact]
        public void RewardsRemaining_Property_ShouldGetAndSet()
        {
            var logic = new MinesweeperLogic();
            logic.RewardsRemaining = 5;
            Assert.Equal(5, logic.RewardsRemaining);
            logic.RewardsRemaining = 2;
            Assert.Equal(2, logic.RewardsRemaining);
        }

        // Test Cells property
        [Fact]
        public void Cells_Property_ShouldReturnCells()
        {
            var board = new BoardModel(1);
            var logic = new MinesweeperLogic();
            logic.GetBoard(board);
            Assert.Equal(board.Cells, logic.Cells);
        }

        // Test DecrementRewards method
        [Fact]
        public void DecrementRewards_ShouldDecreaseRewardsRemaining()
        {
            var logic = new MinesweeperLogic();
            logic.RewardsRemaining = 3;
            logic.DecrementRewards();
            Assert.Equal(2, logic.RewardsRemaining);
        }

        // Test ResetRewards method
        [Fact]
        public void ResetRewards_ShouldSetRewardsRemainingToZero()
        {
            var logic = new MinesweeperLogic();
            logic.RewardsRemaining = 7;
            logic.ResetRewards();
            Assert.Equal(0, logic.RewardsRemaining);
        }

        // Test the GetSize method to ensure it sets the size correctly
        [Fact]
        public void GetSize_ShouldReturnSize()
        {
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();

            // Call the GetSize method with a specific size
            minesweeperLogic.GetSize(10);

            // Assert that the Size property is set correctly
            Assert.Equal(10, minesweeperLogic.Size);
        }

        // Test the SetupBombs method to ensure it places bombs correctly
        [Fact]
        public void SetupBombs_ShouldPlaceBombs()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the number of bombs placed on the board
            int bombCount = 0;
            // Iterate through the board cells to count bombs
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.Cells[i, j] is BombCellModel)
                    {
                        bombCount++;
                    }
                }
            }
            // Assert that the number of bombs placed matches the expected count for the difficulty level
            Assert.Equal(3, bombCount);
        }

        // Test the CountBombs method to ensure it counts bombs around cells correctly
        [Fact]
        public void CountBombs_ShouldCountBombsAroundCells()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Call the CountBombs method to count the number of bombs around each cell
            minesweeperLogic.CountBombs();
            // Check that the number of bombs around a cell is correct
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // If the cell does not contain a bomb, count the number of bombs around it and assert that it matches the expected count
                    if (!(board.Cells[i, j] is BombCellModel))
                    {
                        int expectedCount = 0;
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                if (i + x >= 0 && i + x < board.Size && j + y >= 0 && j + y < board.Size)
                                {
                                    if (board.Cells[i + x, j + y] is BombCellModel)
                                    {
                                        expectedCount++;
                                    }
                                }
                            }
                        }
                        Assert.Equal(expectedCount, board.Cells[i, j].NumberOfBombNeighbors);
                    }
                }
            }
        }

        // Test the PrintAnswers method to ensure it prints the board correctly
        [Fact]
        public void PrintAnswers_ShouldPrintBoard()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Call the CountBombs method to count the number of bombs around each cell
            minesweeperLogic.CountBombs();

            // Read the output of the PrintAnswers method to verify it prints the board correctly
            using (var sw = new StringWriter())
            {
                // Redirect the console output to the StringWriter
                Console.SetOut(sw);
                // Call the PrintAnswers method to print the board
                minesweeperLogic.PrintAnswers(true);

                // Get the output as a string
                var output = sw.ToString();

                // Verify output is not empty
                Assert.False(string.IsNullOrWhiteSpace(output));

                // Verify output contains expected structural elements
                Assert.Contains("+---+", output);
                Assert.Contains("|", output);
            }
        }

        // Test the UpdateCell method to ensure it marks a cell as visited when checking
        [Fact]
        public void UpdateCell_ShouldMarkCellAsVisited()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the board model for the game logic
            minesweeperLogic.GetBoard(board);
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();

            // Verify the cell is not visited initially
            Assert.False(board.Cells[0, 0].isVisited);

            // Call UpdateCell with checkOrFlag = 1 to mark the cell as visited
            minesweeperLogic.UpdateCell(0, 0, 1);

            // Assert that the cell is now marked as visited
            Assert.True(board.Cells[0, 0].isVisited);
        }

        // Test the UpdateCell method to ensure it marks a cell as flagged when flagging
        [Fact]
        public void UpdateCell_ShouldMarkCellAsFlagged()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);

            // Verify the cell is not flagged initially
            Assert.False(board.Cells[0, 0].isFlagged);

            // Call UpdateCell with checkOrFlag = 2 to flag the cell
            minesweeperLogic.UpdateCell(0, 0, 2);

            // Assert that the cell is now marked as flagged
            Assert.True(board.Cells[0, 0].isFlagged);
        }

        // Test the IdentifyCell method to ensure it correctly identifies bomb cells
        [Fact]
        public void IdentifyCell_ShouldReturnTrueIfBomb()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);

            // Find a cell that contains a bomb
            int bombRow = -1;
            int bombCol = -1;

            // Iterate through the columns
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.Cells[i, j] is BombCellModel)
                    {
                        bombRow = i;
                        bombCol = j;
                        break;
                    }
                }
                if (bombRow != -1) break;
            }

            // Assert that we found a cell containing a bomb
            Assert.True(bombRow != -1);

            // Call IdentifyCell on the bomb cell and verify it returns true
            bool result = minesweeperLogic.IdentifyCell(bombCol, bombRow);
            // Assert that the result is true
            Assert.True(result);
        }

        // Test the IdentifyCell method to ensure it correctly identifies non-bomb cells
        [Fact]
        public void IdentifyCell_ShouldReturnFalseIfNotABomb()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);

            // Find a cell that does not contain a bomb
            int nonBombRow = -1;
            int nonBombCol = -1;
            // Iterate through the columns
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (!(board.Cells[i, j] is BombCellModel))
                    {
                        nonBombRow = i;
                        nonBombCol = j;
                        break;
                    }
                }
                if (nonBombRow != -1) break;
            }

            // Assert that we found at cell not containing a bomb
            Assert.True(nonBombRow != -1);

            // Call IdentifyCell on the non bomb cell and verify it returns false
            bool result = minesweeperLogic.IdentifyCell(nonBombCol, nonBombRow);
            // Assert that the result is false
            Assert.False(result);
        }

        // Test the SetBoard method to make sure it sets the board to the program
        [Fact]
        public void SetBoard_ShouldSetBoardProperty()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();

            // Verify the board is null initially
            Assert.Null(minesweeperLogic.board);

            // Call SetBoard to set the board
            minesweeperLogic.GetBoard(board);

            // Assert that the board property is now set correctly
            Assert.Equal(board, minesweeperLogic.board);
        }

        // Test the FloodFill method to ensure it reveals connected empty cells
        [Fact]
        public void FloodFill_ShouldRevealConnectedEmptyCells()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the board model
            minesweeperLogic.GetBoard(board);
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();

            // Find an empty cell to start the flood fill from
            int startRow = -1;
            int startCol = -1;
            // Iterate through the board to find an empty cell
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell is empty
                    if (!(board.Cells[i, j] is BombCellModel) && board.Cells[i, j].NumberOfBombNeighbors == 0)
                    {
                        // Store the coordinates of the empty cell
                        startRow = i;
                        startCol = j;
                        break;
                    }
                }
                // Break the outer loop if an empty cell has been found
                if (startRow != -1) break;
            }

            // Assert that we found an empty cell to start from
            Assert.True(startRow != -1);

            // Verify the cell is not visited
            Assert.False(board.Cells[startRow, startCol].isVisited);

            // Call FloodFill on the empty cell
            minesweeperLogic.FloodFill(board, startRow, startCol);

            // Assert that the starting cell is now visited
            Assert.True(board.Cells[startRow, startCol].isVisited);

            // Assert that at least one additional connected cell was also revealed
            int visitedCount = 0;
            // Iterate through the board to count visited cells
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell has been visited and increment the count
                    if (board.Cells[i, j].isVisited)
                    {
                        // Increment the visited count
                        visitedCount++;
                    }
                }
            }
            // Assert that more than one cell was revealed by the flood fill
            Assert.True(visitedCount > 1);
        }

        // Test the FloodFill method to ensure it does not reveal bomb cells
        [Fact]
        public void FloodFill_ShouldNotRevealBombs()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the board model for the game logic
            minesweeperLogic.GetBoard(board);
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();

            // Find an empty cell to start the flood fill from
            int startRow = -1;
            int startCol = -1;
            // Iterate through the board to find an empty cell
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell is empty
                    if (!(board.Cells[i, j] is BombCellModel) && board.Cells[i, j].NumberOfBombNeighbors == 0)
                    {
                        // Store the coordinates of the empty cell
                        startRow = i;
                        startCol = j;
                        break;
                    }
                }
                // Break the outer loop if an empty cell has been found
                if (startRow != -1) break;
            }

            // Assert that we found an empty cell to start from
            Assert.True(startRow != -1);

            // Call FloodFill on the empty cell
            minesweeperLogic.FloodFill(board, startRow, startCol);

            // Assert that no bomb cells were revealed by the flood fill
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // If the cell is a bomb, verify it was not visited
                    if (board.Cells[i, j] is BombCellModel)
                    {
                        Assert.False(board.Cells[i, j].isVisited);
                    }
                }
            }
        }

        // Test the FloodFill method to ensure it reveals numbered border cells but does not recurse through them
        [Fact]
        public void FloodFill_ShouldRevealNumberedBorderCells()
        {
            // Create a BoardModel with a set difficulty
            BoardModel board = new BoardModel(0);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the board model for the game logic
            minesweeperLogic.GetBoard(board);
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells, 0.07);
            // Count the bombs on the board
            minesweeperLogic.CountBombs();

            // Find an empty cell to start the flood fill from
            int startRow = -1;
            int startCol = -1;
            // Iterate through the board to find an empty cell
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell is empty
                    if (!(board.Cells[i, j] is BombCellModel) && board.Cells[i, j].NumberOfBombNeighbors == 0)
                    {
                        // Store the coordinates of the empty cell
                        startRow = i;
                        startCol = j;
                        break;
                    }
                }
                // Break the outer loop if an empty cell has been found
                if (startRow != -1) break;
            }

            // Assert that we found an empty cell to start from
            Assert.True(startRow != -1);

            // Call FloodFill on the empty cell
            minesweeperLogic.FloodFill(board, startRow, startCol);

            // Check that numbered cells next to the revealed empty cells are also revealed and we do not recurse through them
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // If the cell is visited and has 0 bomb neighbors, check its neighbors
                    if (board.Cells[i, j].isVisited && board.Cells[i, j].NumberOfBombNeighbors == 0)
                    {
                        // Check all 8 neighbors of this empty visited cell
                        for (int k = -1; k <= 1; k++)
                        {
                            // Iterate through each neighboring cell in columns
                            for (int e = -1; e <= 1; e++)
                            {
                                // Calculate the neighbor's row and column
                                int neighborRow = i + k;
                                int neighborCol = j + e;
                                // Check if the neighbor is within bounds
                                if (neighborRow >= 0 && neighborRow < board.Size && neighborCol >= 0 && neighborCol < board.Size)
                                {
                                    // Get the neighboring cell
                                    CellModel neighbor = board.Cells[neighborRow, neighborCol];
                                    // If the neighbor is a numbered cell, it should be revealed
                                    if (!(neighbor is BombCellModel) && neighbor.NumberOfBombNeighbors > 0 && !(neighbor is RewardCellModel))
                                    {
                                        Assert.True(neighbor.isVisited);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}