using System.Collections.Generic;

namespace GuessNumber.Model
{
    public class GameResult
    {
        public string GuessResult { get; set; }
        public GameStatus Status { get; set; }
        public IList<Guess> GuessHistory { get; } = new List<Guess>();
        
        public void AddGuessHistory(Guess guess)
        {
            GuessHistory.Add(guess);
        }
    }
}