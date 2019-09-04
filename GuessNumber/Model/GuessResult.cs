using System.Collections.Generic;

namespace GuessNumber.Model
{
    public class GuessResult
    {
        public string CurrentResult { get; set; }
        public GameResult GameResult { get; set; }
        public IList<Guess> GuessHistory { get; } = new List<Guess>();
        
        public void AddGuessHistory(Guess guess)
        {
            GuessHistory.Add(guess);
        }
    }
}