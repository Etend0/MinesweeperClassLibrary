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
    /// An internal class that provides custom JSON serialization and deserialization for a 2D array of CellModel objects, since System.Text.Json does not natively support multi-dimensional arrays
    /// </summary>
    public class CellModel2DArrayConverter : JsonConverter<CellModel[,]>
    {
        /// <summary>
        /// An override of the Read method that deserializes a JSON array of arrays (jagged array) into a 2D array of CellModel objects
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override CellModel[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the JSON into a jagged array (array of arrays) first
            var jagged = JsonSerializer.Deserialize<CellModel[][]>(ref reader, options);
            // Convert the jagged array into a 2D array
            int rows = jagged.Length;
            // Handle the case where the jagged array might be empty to avoid IndexOutOfRangeException
            int cols = jagged[0].Length;
            // Create a new 2D array and populate it with the values from the jagged array
            var result = new CellModel[rows, cols];
            // Iterate through the jagged array and copy the values into the 2D array
            for (int i = 0; i < rows; i++)
                // Handle the case where rows might have different lengths
                for (int j = 0; j < cols; j++)
                    // Ensure we don't go out of bounds if the jagged array has irregular row lengths
                    result[i, j] = jagged[i][j];
            // Return the populated 2D array
            return result;
        }

        /// <summary>
        /// An override of the Write method that serializes a 2D array of CellModel objects into a JSON array of arrays format
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, CellModel[,] value, JsonSerializerOptions options)
        {
            // Convert the 2D array into a jagged array first, since System.Text.Json can serialize jagged arrays natively
            int rows = value.GetLength(0);
            // Handle the case where the 2D array might be empty to avoid IndexOutOfRangeException
            int cols = value.GetLength(1);
            // Create a jagged array and populate it with the values from the 2D array
            var jagged = new CellModel[rows][];
            // Iterate through the 2D array and copy the values into the jagged array
            for (int i = 0; i < rows; i++)
            {
                // Handle the case where rows might have different lengths by initializing each row of the jagged array separately
                jagged[i] = new CellModel[cols];
                // Iterate through the columns and copy the values from the 2D array to the jagged array
                for (int j = 0; j < cols; j++)
                    // Make sure we don't go out of bounds if the 2D array has irregular row lengths
                    jagged[i][j] = value[i, j];
            }
            // Serialize the jagged array to JSON using the provided options
            JsonSerializer.Serialize(writer, jagged, options);
        }
    }

    /// <summary>
    /// An internal class that provides custom JSON serialization and deserialization for TimeSpan objects, since System.Text.Json does not natively support TimeSpan
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// An override of the Read method that deserializes a JSON string or number into a TimeSpan object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Try to parse as string
            if (reader.TokenType == JsonTokenType.String)
            {
                // Get the string value from the JSON
                var str = reader.GetString();
                // Try parsing as TimeSpan string format (c format)
                if (TimeSpan.TryParse(str, out var ts))
                    return ts;
                // Try parsing as ISO 8601 duration
                if (System.Xml.XmlConvert.ToTimeSpan(str) is TimeSpan xmlTs)
                    return xmlTs;
            }
            // Try to parse as number (ticks)
            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long ticks))
                // If it's a number, treat it as ticks and convert to TimeSpan
                return TimeSpan.FromTicks(ticks);
            // If we can't parse it as either a string or a number, throw an exception indicating that the conversion failed
            throw new JsonException($"Unable to convert {reader.TokenType} to TimeSpan");
        }

        /// <summary>
        /// An override of the Write method that serializes a TimeSpan object into a JSON string in the standard TimeSpan format (c format)
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            // Write as string in c format
            writer.WriteStringValue(value.ToString());
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
                // Read all lines from the file into an array of strings
                string[] lines = File.ReadAllLines(filePath);
                // Set the id to null
                int? id = null;
                // Set the name to null
                string name = null;
                // Set the score to null
                int? score = null;
                // Set the date to null
                DateTime? date = null;
                // Set the time to null
                TimeSpan? time = null;
                // Iterate through the lines and parse the properties of each GameState object
                foreach (string line in lines)
                {
                    // Check if the line is empty or whitespace, which indicates the end of a GameState entry
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // If we have all the necessary properties, create a new GameState object and add it to the _playerScores list
                        if (id.HasValue && name != null && score.HasValue && date.HasValue)
                        {
                            // Create a new GameState object using the parsed properties, using TimeSpan.Zero as a default value for time if it is null
                            var gameState = new GameState(id.Value, name, score.Value, time ?? TimeSpan.Zero);
                            // Use reflection to set the Date property of the GameState object, since it is protected and cannot be set directly
                            typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                            // Add the GameState object to the _playerScores list
                            _playerScores.Add(gameState);
                        }
                        // Reset the properties for the next GameState entry
                        id = null; name = null; score = null; date = null; time = null;
                        continue;
                    }
                    // Check if the line starts with the Id
                    if (line.StartsWith("Id:"))
                        // If so, parse the Id value from the line and assign it to the id variable
                        id = int.Parse(line.Substring(3).Trim());
                    // Check if the line starts with the Name
                    else if (line.StartsWith("Name:"))
                        // If so, parse the Name value from the line and assign it to the name variable
                        name = line.Substring(5).Trim();
                    // Check if the line starts with the Score
                    else if (line.StartsWith("Score:"))
                        // If so, parse the Score value from the line and assign it to the score variable
                        score = int.Parse(line.Substring(6).Trim());
                    // Check if the line starts with the Date
                    else if (line.StartsWith("Date:"))
                        // If so, parse the Date value from the line and assign it to the date variable
                        date = DateTime.Parse(line.Substring(5).Trim());
                    // Check if the line starts with the Time
                    else if (line.StartsWith("Time:"))
                        // If so, parse the Time value from the line and assign it to the time variable
                        time = TimeSpan.Parse(line.Substring(5).Trim());
                }
                // Check if we have all the necessary properties for the last GameState entry after the loop
                if (id.HasValue && name != null && score.HasValue && date.HasValue)
                {
                    // Create a new GameState object using the parsed properties, using TimeSpan.Zero as a default value for time if it is null
                    var gameState = new GameState(id.Value, name, score.Value, time ?? TimeSpan.Zero);
                    // Use reflection to set the Date property of the GameState object, since it is protected and cannot be set directly
                    typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                    // Add the GameState object to the list
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
        /// Save the current game progress (BoardModel and GameState) to a JSON file.
        /// </summary>
        /// <param name="board">The board to save.</param>
        /// <param name="gameState">The game state to save.</param>
        /// <param name="filename">The filename for the save file (without path).</param>
        /// <returns>True if save is successful, false otherwise.</returns>
        public bool SaveGameProgress(BoardModel board, GameState gameState, int rewards, string filename)
        {
            // Get the file path for the save file
            string filePath = filename;
            // If the filename is not an absolute path, combine it with the App_Data directory in the application's base directory
            if (!Path.IsPathRooted(filename))
            {
                // Define the directory path for the App_Data folder
                string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                // Check if the directory exists, if not create it
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                // Combine the directory path with the filename to get the full file path for the save file
                filePath = Path.Combine(dirPath, filename);
            }
            // Enter a try-catch block to handle potential exceptions during file writing
            try
            {
                // Create an object that contains the BoardModel, GameState, and Rewards to be saved, which will be serialized to JSON
                var saveObj = new { Board = board, GameState = gameState, Rewards = rewards };
                // Create JsonSerializerOptions and add the custom converters for CellModel polymorphism, CellModel[,] and TimeSpan
                var options = new JsonSerializerOptions { WriteIndented = true };
                options.Converters.Add(new CellModelJsonConverter());
                options.Converters.Add(new CellModel2DArrayConverter());
                options.Converters.Add(new TimeSpanConverter());
                // Serialize the save object to a JSON string using the specified options, which will include the custom converters for handling the complex types
                string json = JsonSerializer.Serialize(saveObj, options);
                // Write the JSON string to the specified file path, overwriting any existing content in the file
                File.WriteAllText(filePath, json);
                // If writing is successful, return true
                return true;
            }
            // If we get an exception, catch it and throw a new exception with a message
            catch (Exception ex)
            {
                // Thorw the exception message
                throw new Exception($"Save failed: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Load game progress from a JSON file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="board"></param>
        /// <param name="gameState"></param>
        /// <param name="rewards"></param>
        /// <returns></returns>
        public bool LoadGameProgress(string filename, out BoardModel board, out GameState gameState, out int rewards)
        {
            // Set board to null
            board = null;
            // Set gameState to null
            gameState = null;
            // Set rewards to 0
            rewards = 0;
            // Get the file path for the save file
            string filePath = filename;
            // If the filename is not an absolute path, combine it with the App_Data directory in the application's base directory
            if (!Path.IsPathRooted(filename))
            {
                // Define the directory path for the App_Data folder
                string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                // Combine the directory path with the filename to get the full file path for the save file
                filePath = Path.Combine(dirPath, filename);
            }
            // Check if the file exists, if not return false
            if (!File.Exists(filePath))
                // If the file does not exist, return false to indicate that the load operation was unsuccessful
                return false;
            // Enter a try-catch block to handle potential exceptions during file reading and deserialization
            try
            {
                // Read the entire content of the file into a string variable called json, which will contain the JSON representation of the saved game progress
                string json = File.ReadAllText(filePath);
                // Create JsonSerializerOptions and add the custom converters for CellModel polymorphism, CellModel[,] and TimeSpan
                var options = new JsonSerializerOptions { WriteIndented = true };
                options.Converters.Add(new CellModelJsonConverter());
                options.Converters.Add(new CellModel2DArrayConverter());
                options.Converters.Add(new TimeSpanConverter());
                // Parse the JSON string into a JsonDocument to access its properties and deserialize the BoardModel and GameState objects using the specified options with the custom converters
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    // Get the root element of the JSON document, which contains the properties for Board, GameState, and Rewards
                    var root = doc.RootElement;
                    // Get the JSON elements for the Board and GameState properties from the root element
                    var boardElem = root.GetProperty("Board").GetRawText();
                    // Get the JSON element for the GameState property from the root element
                    var gameStateElem = root.GetProperty("GameState").GetRawText();
                    // Deserialize the BoardModel and GameState objects from their respective JSON elements using the specified options with the custom converters, and assign them to the out parameters
                    board = JsonSerializer.Deserialize<BoardModel>(boardElem, options);
                    // Deserialize the GameState object from its JSON element using the specified options with the custom converters, and assign it to the out parameter
                    gameState = JsonSerializer.Deserialize<GameState>(gameStateElem, options);
                    // Check if the Rewards property exists in the JSON, and if so, parse its value and assign it to the rewards out parameter
                    if (root.TryGetProperty("Rewards", out var rewardsElem))
                        // If the Rewards property exists, parse its value as an integer and assign it to the rewards out parameter
                        rewards = rewardsElem.GetInt32();
                }
                // If reading and deserialization are successful, return true
                return true;
            }
            // If we get an exception, catch it and throw a new exception with a message
            catch (Exception ex)
            {
                // Throw the exception message
                throw new Exception($"Load failed: {ex.Message}", ex);
            }
        }
    }
}
