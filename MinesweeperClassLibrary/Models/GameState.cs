using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperClassLibrary.Models
{
    public class GameState
    {
        public int Id { get; protected set; }
        public String Name { get; protected set; }
        public int Score { get; protected set; }
        public DateTime Date { get; protected set; }
        public TimeSpan Time { get; protected set; }

        public GameState(int id, String name, int score, TimeSpan Time)
        {
            Id = id;
            Name = name;
            Score = score;
            this.Date = DateTime.Now;
            this.Time = Time;
        }

        public void setId(int id) { Id = id; }

        public int getId() { return Id; }
        public string getName() { return Name; }
        public int getScore() { return Score; }
        public DateTime getDate() { return Date; }
        public TimeSpan getTime() { return Time; }
    }
}
