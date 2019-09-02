using System;

namespace GuessNumber.Exceptions
{
    public class DuplicatedAnswerException : Exception
    {
        public DuplicatedAnswerException():base("The value of answer can not be duplicated.")
        {
        }
    }
}