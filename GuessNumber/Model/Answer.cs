using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            var values = new List<int> {value1, value2, value3, value4};
            foreach (var i in values)
            {
                if (values.Where(j => j >= i).Any(j => i == j))
                {
                    throw new DuplicatedAnswerException();
                }
            }
        }

        private static bool NotInRange(int value)
        {
            return value < 0 || value > 9;
        }
    }
}