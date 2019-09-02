using System;

namespace GuessNumber.Exceptions
{
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException(): base("The value is out of range.")
        {
        }
    }
}