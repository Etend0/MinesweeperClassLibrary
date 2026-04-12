using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MinesweeperClassLibrary.Models;
using System.IO;

namespace MinesweeperClassLibrary.Services.MinesweeperDAO
{
    /// <summary>
    /// Custom JSON converter for 2D CellModel arrays
    /// </summary>
    public class CellModelArrayConverter : JsonConverter<CellModel[,]>
    {
        /// <summary>
        /// Method to read a JSON array and convert it to a 2D CellModel array. The JSON is expected to be an array of arrays, where each inner array represents a row of the 2D array.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override CellModel[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the outer array
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            // Set up a list to hold the rows of the 2D array as we read them from the JSON
            var rows = new List<List<CellModel>>();

            // Read each row from the JSON and convert it to a List<CellModel>, then add it to the rows list
            while (reader.Read())
            {
                // If we reach the end of the outer array, break out of the loop
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;

                // If the token is the start of an inner array, read it as a row of CellModel objects
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    // Set up a list to hold the CellModel objects for this row
                    var row = new List<CellModel>();
                    // Read each CellModel object from the inner array and add it to the row list
                    while (reader.Read())
                    {
                        // If we reach the end of the inner array, break out of the loop
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;

                        // Deserialize the current JSON object to a CellModel and add it to the row list
                        var cell = JsonSerializer.Deserialize<CellModel>(ref reader, options);
                        // Add the deserialized CellModel to the current row
                        row.Add(cell);
                    }
                    rows.Add(row);
                }
            }

            // Check if we have read any rows, if not return null
            if (rows.Count == 0)
                return null;

            // Convert the list of rows to a 2D array of CellModel
            int rowCount = rows.Count;
            // If all rows have the same number of columns, get the column count from the first row
            int colCount = rows[0].Count;
            // If not all rows have the same number of columns, throw an exception
            var result = new CellModel[rowCount, colCount];

            // Iterate through the rows and columns and copy the CellModel objects from the list to the 2D array
            for (int i = 0; i < rowCount; i++)
            {
                // Check if the current row has the same number of columns as the first row, if not throw an exception
                for (int j = 0; j < colCount; j++)
                {
                    // Copy the CellModel object from the list to the 2D array
                    result[i, j] = rows[i][j];
                }
            }

            return result;
        }

        /// <summary>
        /// Method to write a 2D CellModel array to JSON. The 2D array will be serialized as an array of arrays, where each inner array represents a row of the 2D array.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, CellModel[,] value, JsonSerializerOptions options)
        {
            // If the value is null, write a JSON null value and return
            if (value == null)
            {
                // Write a JSON null value to indicate that the 2D array is null
                writer.WriteNullValue();
                return;
            }

            // Start writing the outer array to represent the 2D array
            writer.WriteStartArray();

            // Get the number of rows and columns in the 2D array
            int rows = value.GetLength(0);
            // Assuming all rows have the same number of columns, get the column count from the first row
            int cols = value.GetLength(1);

            // Iterate through the rows and columns of the 2D array and write each CellModel object to the JSON as part of an inner array representing the row
            for (int i = 0; i < rows; i++)
            {
                // Start writing an inner array to represent the current row of the 2D array
                writer.WriteStartArray();
                // Iterate through the columns of the current row and write each CellModel object to the JSON
                for (int j = 0; j < cols; j++)
                {
                    // Serialize the current CellModel object to JSON and write it to the inner array
                    JsonSerializer.Serialize(writer, value[i, j], options);
                }
                // After writing all the CellModel objects for the current row, end the inner array
                writer.WriteEndArray();
            }
            // After writing all the rows, end the outer array
            writer.WriteEndArray();
        }
    }

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
                            $"Date: {score.Date:O}\n" +
                            $"Time: {score.Time}\n\n";

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
                // TimeSpan to hold the current GameState objects time
                TimeSpan? time = null;
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
                            var gameState = new GameState(id.Value, name, score.Value, time ?? TimeSpan.Zero);
                            // Use reflection to set the Date property since it has a protected setter
                            typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                            // Add the GameState object to the _playerScores list
                            _playerScores.Add(gameState);
                        }
                        // Reset the variables for the next GameState object
                        id = null; name = null; score = null; date = null; time = null;
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
                    // If the line starts with "Time:", parse the time
                    else if (line.StartsWith("Time:"))
                        // Set the time to the string after "Time:" and trim any whitespace, then parse it to a TimeSpan
                        time = TimeSpan.Parse(line.Substring(5).Trim());
                }
                // After the loop, check if there is a GameState object that has been parsed but not added to the list
                if (id.HasValue && name != null && score.HasValue && date.HasValue)
                {
                    // If so, create a new GameState object and add it to the _playerScores list
                    var gameState = new GameState(id.Value, name, score.Value, time ?? TimeSpan.Zero);
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

        /// <summary>
        /// Method to save the game state to a JSON file
        /// </summary>
        /// <param name="gameSave"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SaveGame(GameSave gameSave, string filePath)
        {
            // Enter a try-catch block to handle potential exceptions during file writing
            try
            {
                // Check if the directory exists, if not create it
                string directory = Path.GetDirectoryName(filePath);
                // If the directory is not null or empty and does not exist, create it
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    // Create the directory
                    Directory.CreateDirectory(directory);
                }

                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    // Set WriteIndented to true for better readability of the JSON file
                    WriteIndented = true,
                    // IncludeFields is set to true to allow serialization of fields in the GameSave class
                    IncludeFields = true,
                    // Add the custom converter for CellModel[,] to handle serialization and deserialization of the 2D array
                    Converters = { new CellModelArrayConverter() }
                };

                // Serialize the GameSave object to JSON
                string jsonString = JsonSerializer.Serialize(gameSave, options);

                // Write the JSON string to file
                File.WriteAllText(filePath, jsonString);

                return true;
            }
            // If an exception occurs during file writing, log the error and throw to show the error message in the GUI
            catch (Exception ex)
            {
                // Log the error to console
                Console.WriteLine($"Error saving game: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Method to load the game state from a JSON file and return a GameSave object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public GameSave LoadGame(string filePath)
        {
            // Enter a try-catch block to handle potential exceptions during file reading and deserialization
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    return null;
                }

                // Read the JSON string from file
                string jsonString = File.ReadAllText(filePath);

                // Configure JSON deserialization options
                var options = new JsonSerializerOptions
                {
                    // Set the IncludeFields to true to allow deserialization of fields in the GameSave class
                    IncludeFields = true,
                    // Add the custom converter for CellModel[,] to handle serialization and deserialization of the 2D array
                    Converters = { new CellModelArrayConverter() }
                };

                // Deserialize the JSON string to GameSave object
                GameSave gameSave = JsonSerializer.Deserialize<GameSave>(jsonString, options);

                return gameSave;
            }
            // If an exception occurs during file reading or deserialization, log the error and throw to show the error message in the GUI
            catch (Exception ex)
            {
                // Log the error to console
                Console.WriteLine($"Error loading game: {ex.Message}");
                throw;
            }
        }
    }
}
