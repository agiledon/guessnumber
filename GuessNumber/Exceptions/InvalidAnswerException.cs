using System;

namespace GuessNumber.Exceptions
{
    public class InvalidAnswerException: Exception
    {
        public InvalidAnswerException(string message) : base(message)
        {
        }
    }
}