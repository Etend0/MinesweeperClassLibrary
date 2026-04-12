using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperClassLibrary.Models
{
    /// <summary>
    /// Model class to represent a saved game state
    /// </summary>
    public class GameSave
    {
        public BoardModel Board { get; set; }
        public int ElapsedSeconds { get; set; }
        public int GameDifficulty { get; set; }
        public string GameState { get; set; }
        public bool Victory { get; set; }
        public bool Death { get; set; }
        public int RewardsRemaining { get; set; }
        public DateTime SavedDate { get; set; }

        /// <summary>
        /// Default constructor for JSON serialization
        /// </summary>
        public GameSave()
        {
            Board = null;
            ElapsedSeconds = 0;
            GameDifficulty = 0;
            GameState = " ";
            Victory = false;
            Death = false;
            RewardsRemaining = 0;
            SavedDate = DateTime.Now;
        }

        /// <summary>
        /// Parameterized constructor for creating a game save
        /// </summary>
        public GameSave(BoardModel board, int elapsedSeconds, int gameDifficulty, string gameState, bool victory, bool death, int rewardsRemaining)
        {
            Board = board;
            ElapsedSeconds = elapsedSeconds;
            GameDifficulty = gameDifficulty;
            GameState = gameState;
            Victory = victory;
            Death = death;
            RewardsRemaining = rewardsRemaining;
            SavedDate = DateTime.Now;
        }
    }
}
