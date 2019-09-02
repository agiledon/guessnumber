using GuessNumber.Exceptions;

namespace GuessNumber.Model
{
    public class Answer
    {
        public static void Of(int value1, int value2, int value3, int value4)
        {
            if (NotInRange(value1) 
                || NotInRange(value2) 
                || NotInRange(value3) 
                || NotInRange(value4))
            {
                throw new OutOfRangeException();
            }
        }

        private static bool NotInRange(int value)
        {
            return value < 0 || value > 9;
        }
    }
}