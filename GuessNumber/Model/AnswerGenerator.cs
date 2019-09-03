using System.Collections.Generic;

namespace GuessNumber.Model
{
    public class AnswerGenerator
    {
        private readonly IRandomIntNumber _random;

        public AnswerGenerator(IRandomIntNumber random)
        {
            _random = random;
        }

        public Answer Generate()
        {
            IList<int> numbers = new List<int>(4);
            var number = _random.Next();
            numbers.Add(number);

            for (int i = 0; i < 3; i++)
            {
                int nextNumber;
                do
                {
                    nextNumber = _random.Next();
                } while (numbers.Contains(nextNumber) || NotInRange(nextNumber));
                numbers.Add(nextNumber);
            }
            return Answer.Of(numbers);
        }

        private bool NotInRange(int number)
        {
            return number < 0 || number > 9;
        }
    }
}