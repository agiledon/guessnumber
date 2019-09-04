using System;

namespace GuessNumber.Model
{
    public class Guess : IEquatable<Guess>
    {
        public Answer InputAnswer;
        public string Result;

        public Guess(Answer inputAnswer, string result)
        {
            InputAnswer = inputAnswer;
            Result = result;
        }

        public bool Equals(Guess other)
        {
            if (other == null)
            {
                return false;
            }
            return other.InputAnswer.ToString().Equals(InputAnswer.ToString()) &&
                   other.Result.Equals(Result);
        }
    }
}