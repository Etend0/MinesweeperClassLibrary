using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperClassLibrary.Models;
using System.IO;

namespace MinesweeperClassLibrary.Services.MinesweeperDAO
{
    public class MinesweeperDAO
    {
        // Class level variables
        private List<GameState> _playerScores;

        /// <summary>
        /// Constructor for MinesweeperDAO
        /// </summary>
        public MinesweeperDAO()
        {
            // Initialize the _playerScores list
            _playerScores = new List<GameState>();
        }

        /// <summary>
        /// Add a player score to the list
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public int AddPlayerScore(GameState gameState)
        {
            // Get the current size of the _playerScores list
            int listSize = _playerScores.Count;

            // Check if the list is not empty
            if (listSize > 0)
            {
                // If not, set the Id of the next GameState object to the current size of the list to reflect its index in the list
                gameState.setId(listSize);
            }
            // Add the GameState object to the _playerScores list
            _playerScores.Add(gameState);
            // Return the new size of the _playerScores list
            return _playerScores.Count;
        }

        /// <summary>
        /// Method to get the list of player scores
        /// </summary>
        /// <returns></returns>
        public List<GameState> GetScoresList()
        {
            // Return the _playerScores list
            return _playerScores;
        }

        /// <summary>
        /// Method to clear the list
        /// </summary>
        public void ClearScores()
        {
            // Clear the _playerScores list
            _playerScores.Clear();
        }

        /// <summary>
        /// Method to write player scores to the text file
        /// </summary>
        /// <returns></returns>
        public bool WriteScoreToFile()
        {
            // Define the directory
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            // Define the file path
            string filePath = Path.Combine(dirPath, "PlayerScores.txt");

            // Check if the directory exists, if not create it
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            // Enter a try-catch block to handle potential exceptions during file writing
            try
            {
                // Use a StreamWriter to write the player scores to the file, overwriting any existing content
                using (StreamWriter streamWriter = new StreamWriter(filePath, append: false))
                {
                    // Iterate through the _playerScores list and write each GameState's properties to the file in a readable format
                    foreach (var score in _playerScores)
                    {
                        // Create a string representation of the GameState object
                        string scoreString =
                            $"Id: {score.Id}\n" +
                            $"Name: {score.Name}\n" +
                            $"Score: {score.Score}\n" +
                            $"Date: {score.Date:O}\n\n";

                        // Write the string to the file
                        streamWriter.Write(scoreString);
                    }
                }
                // If writing is successful, return true
                return true;
            }
            catch
            {   
                // If an exception occurs during file writing, return false
                return false;
            }
        }

        /// <summary>
        /// Reads the player scores from the PlayerScores.txt file and populates the _playerScores list.
        /// </summary>
        /// <returns>True if read is successful, false otherwise.</returns>
        public bool ReadScoresFromFile()
        {
            // Define the file path
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "PlayerScores.txt");
            // Check if the file exists, if not return false
            if (!File.Exists(filePath))
                return false;

            // Clear the _playerScores list before populating it with data from the file
            _playerScores.Clear();

            // Enter a try-catch block to handle potential exceptions during file reading
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);
                // Int to hold the current GameState objects id
                int? id = null;
                // String to hold the current GameState objects name
                string name = null;
                // Int to hold the current GameState objects score
                int? score = null;
                // DateTime to hold the current GameState objects date
                DateTime? date = null;
                // Iterate through the lines and parse the properties of each GameState object
                foreach (string line in lines)
                {
                    // Check if the line is empty, if so create a GameState object in that position
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // If all properties have been parsed, create a new GameState object and add it to the _playerScores list
                        if (id.HasValue && name != null && score.HasValue && date.HasValue)
                        {
                            // Create a new GameState object with the results
                            var gameState = new GameState(id.Value, name, score.Value, TimeSpan.Zero);
                            // Use reflection to set the Date property since it has a protected setter
                            typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                            // Add the GameState object to the _playerScores list
                            _playerScores.Add(gameState);
                        }
                        // Reset the variables for the next GameState object
                        id = null; name = null; score = null; date = null;
                        // Continue to the next line
                        continue;
                    }
                    // If the line starts with "Id:", parse the id
                    if (line.StartsWith("Id:"))
                        // Set the id to the string after "Id:" and trim any whitespace, then parse it to an int
                        id = int.Parse(line.Substring(3).Trim());
                    // If the line starts with "Name:", parse the name
                    else if (line.StartsWith("Name:"))
                        // Set the name to the string after "Name:" and trim any whitespace
                        name = line.Substring(5).Trim();
                    // If the line starts with "Score:", parse the score
                    else if (line.StartsWith("Score:"))
                        // Set the score to the string after "Score:" and trim any whitespace, then parse it to an int
                        score = int.Parse(line.Substring(6).Trim());
                    // If the line starts with "Date:", parse the date
                    else if (line.StartsWith("Date:"))
                        // Set the date to the string after "Date:" and trim any whitespace, then parse it to a DateTime
                        date = DateTime.Parse(line.Substring(5).Trim());
                }
                // After the loop, check if there is a GameState object that has been parsed but not added to the list
                if (id.HasValue && name != null && score.HasValue && date.HasValue)
                {
                    // If so, create a new GameState object and add it to the _playerScores list
                    var gameState = new GameState(id.Value, name, score.Value, TimeSpan.Zero);
                    // Use reflection to set the Date property since it has a protected setter
                    typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                    // Add the GameState object to the _playerScores list
                    _playerScores.Add(gameState);
                }
                // If reading is successful, return true
                return true;
            }
            catch
            {
                // If an exception occurs during file reading, return false
                return false;
            }
        }
    }
}
