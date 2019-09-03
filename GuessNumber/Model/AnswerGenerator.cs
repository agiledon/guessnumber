using System.Collections.Generic;

namespace GuessNumber.Model
{
    public class AnswerGenerator
    {
        private readonly IRandomIntNumber _random;
        private const int AnswerSize = 4;

        public AnswerGenerator(IRandomIntNumber random)
        {
            _random = random;
        }

        public Answer Generate()
        {
            IList<int> numbers = new List<int>(AnswerSize);

            for (var i = 0; i < AnswerSize; i++)
            {
                numbers.Add(GenerateUniqueCorrectNumber(numbers));
            }

            return Answer.Of(numbers);
        }

        private int GenerateUniqueCorrectNumber(IList<int> numbers)
        {
            int nextNumber;
            do
            {
                nextNumber = _random.Next();
            } while (numbers.Contains(nextNumber) || NotInRange(nextNumber));

            return nextNumber;
        }

        private bool NotInRange(int number)
        {
            return number < 0 || number > 9;
        }
    }
}