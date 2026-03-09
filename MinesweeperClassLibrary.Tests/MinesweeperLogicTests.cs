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
                minesweeperLogic.PrintAnswers();

                // Get the output as a string
                var output = sw.ToString();

                // Verify output is not empty
                Assert.False(string.IsNullOrWhiteSpace(output));

                // Verify output contains expected structural elements
                Assert.Contains("+---+", output);
                Assert.Contains("|", output);
            }
        }
    }
}