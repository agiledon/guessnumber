using System;

namespace GuessNumber.Exceptions
{
    public class InvalidCountException : Exception
    {
        public InvalidCountException(): base("Invalid count of number.")
        {
        }
    }
}