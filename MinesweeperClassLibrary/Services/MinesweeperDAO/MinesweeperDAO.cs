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
            _playerScores = new List<GameState>();
        }

        /// <summary>
        /// Add a player score to the list
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns></returns>
        public int AddPlayerScore(GameState gameState)
        {
            int listSize = _playerScores.Count;

            if (listSize > 0)
            {
                gameState.setId(listSize);
            }
            _playerScores.Add(gameState);
            return _playerScores.Count;
        }

        /// <summary>
        /// Get the list of player scores
        /// </summary>
        /// <returns></returns>
        public List<GameState> GetScoresList()
        {
            // Return the _playerScores list
            return _playerScores;
        }

        public void ClearScores()
        {
            _playerScores.Clear();
        }

        public bool WriteScoreToFile()
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            string filePath = Path.Combine(dirPath, "PlayerScores.txt");

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            try
            {
                // Option 1: Overwrite the file with all current scores (cleanest)
                using (StreamWriter streamWriter = new StreamWriter(filePath, append: false))  // false = overwrite
                {
                    foreach (var score in _playerScores)   // Use DAO's own list
                    {
                        string scoreString =
                            $"Id: {score.Id}\n" +
                            $"Name: {score.Name}\n" +
                            $"Score: {score.Score}\n" +
                            $"Date: {score.Date:O}\n\n";

                        streamWriter.Write(scoreString);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the player scores from the PlayerScores.txt file and populates the _playerScores list.
        /// </summary>
        /// <returns>True if read is successful, false otherwise.</returns>
        public bool ReadScoresFromFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "PlayerScores.txt");
            if (!File.Exists(filePath))
                return false;

            _playerScores.Clear();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int? id = null;
                string name = null;
                int? score = null;
                DateTime? date = null;
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // End of one record, create GameState if all fields are present
                        if (id.HasValue && name != null && score.HasValue && date.HasValue)
                        {
                            // Use a default TimeSpan since it's not stored
                            var gameState = new GameState(id.Value, name, score.Value, TimeSpan.Zero);
                            // Set the Date property via reflection since setter is protected
                            typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                            _playerScores.Add(gameState);
                        }
                        id = null; name = null; score = null; date = null;
                        continue;
                    }
                    if (line.StartsWith("Id:"))
                        id = int.Parse(line.Substring(3).Trim());
                    else if (line.StartsWith("Name:"))
                        name = line.Substring(5).Trim();
                    else if (line.StartsWith("Score:"))
                        score = int.Parse(line.Substring(6).Trim());
                    else if (line.StartsWith("Date:"))
                        date = DateTime.Parse(line.Substring(5).Trim());
                }
                // Handle last record if file does not end with blank line
                if (id.HasValue && name != null && score.HasValue && date.HasValue)
                {
                    var gameState = new GameState(id.Value, name, score.Value, TimeSpan.Zero);
                    typeof(GameState).GetProperty("Date").SetValue(gameState, date.Value);
                    _playerScores.Add(gameState);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
