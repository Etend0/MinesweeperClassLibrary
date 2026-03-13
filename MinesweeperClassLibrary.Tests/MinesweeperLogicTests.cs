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
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells);
            // Count the number of bombs placed on the board
            int bombCount = 0;
            // Iterate through the board cells to count bombs
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell contains a bomb and increment the bomb count
                    if (board.Cells[i, j].isBomb == true)
                    {
                        // Increment the bomb count if a bomb is found
                        bombCount++;
                    }
                }
            }
            // Assert that the number of bombs placed matches the expected count for the difficulty level
            Assert.Equal(15, bombCount);
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
            minesweeperLogic.SetupBombs(board.Cells);
            // Call the CountBombs method to count the number of bombs around each cell
            minesweeperLogic.CountBombs();
            // Check that the number of bombs around a cell is correct
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // If the cell does not contain a bomb, count the number of bombs around it and assert that it matches the expected count
                    if (board.Cells[i, j].isBomb == false)
                    {
                        // Initialize the expected bomb count to zero
                        int expectedCount = 0;
                        // Iterate through the neighboring cells to count the number of bombs
                        for (int x = -1; x <= 1; x++)
                        {
                            // Iterate through the neighboring cells in the y-direction
                            for (int y = -1; y <= 1; y++)
                            {
                                // Check if the neighboring cell is within the bounds of the board
                                if (i + x >= 0 && i + x < board.Size && j + y >= 0 && j + y < board.Size)
                                {
                                    // If the neighboring cell contains a bomb, increment the expected bomb count
                                    if (board.Cells[i + x, j + y].isBomb == true)
                                    {
                                        // Increment the expected bomb count if a bomb is found in the neighboring cell
                                        expectedCount++;
                                    }
                                }
                            }
                        }
                        // Assert that the number of bombs around the cell matches the expected count
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
            minesweeperLogic.SetupBombs(board.Cells);
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
            BoardModel board = new BoardModel(1);
            // Create an instance of MinesweeperLogic
            MinesweeperLogic minesweeperLogic = new MinesweeperLogic();
            // Set the size of the board
            minesweeperLogic.GetSize(board.Size);
            // Call the SetupBombs method to place bombs on the board
            minesweeperLogic.SetupBombs(board.Cells);
            // Set the Cells property in the logic class
            minesweeperLogic.Cells = board.Cells;

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
            minesweeperLogic.SetupBombs(board.Cells);
            // Set the Cells property in the logic class
            minesweeperLogic.Cells = board.Cells;

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
            minesweeperLogic.SetupBombs(board.Cells);
            // Set the Cells property in the logic class
            minesweeperLogic.Cells = board.Cells;

            // Find a cell that contains a bomb
            int bombRow = -1;
            int bombCol = -1;

            // Iterate through the columns
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell contains a bomb and store its coordinates
                    if (board.Cells[i, j].isBomb)
                    {
                        // Store the coordinates of the bomb cell
                        bombRow = i;
                        bombCol = j;
                        break;
                    }
                }
                // Break the outer loop if a bomb cell has been found
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
            minesweeperLogic.SetupBombs(board.Cells);
            // Set the Cells property in the logic class
            minesweeperLogic.Cells = board.Cells;

            // Find a cell that does not contain a bomb
            int nonBombRow = -1;
            int nonBombCol = -1;
            // Iterate through the columns
            for (int i = 0; i < board.Size; i++)
            {
                // Iterate through each cell in the row
                for (int j = 0; j < board.Size; j++)
                {
                    // Check if the cell does not contain a bomb and store its coordinates
                    if (!board.Cells[i, j].isBomb)
                    {
                        // Store the coordinates of the non-bomb cell
                        nonBombRow = i;
                        nonBombCol = j;
                        break;
                    }
                }
                // Break the outer loop if a non bomb cell has been found
                if (nonBombRow != -1) break;
            }

            // Assert that we found at cell not containing a bomb
            Assert.True(nonBombRow != -1);

            // Call IdentifyCell on the non bomb cell and verify it returns false
            bool result = minesweeperLogic.IdentifyCell(nonBombCol, nonBombRow);
            // Assert that the result is false
            Assert.False(result);
        }
    }
}