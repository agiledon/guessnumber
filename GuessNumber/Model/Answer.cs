using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GuessNumber.Exceptions;

namespace GuessNumber.Model
{
    public class Answer
    {
        private int number1;
        private int number2;
        private int number3;
        private int number4;
        private IList<int> numbers;

        public IList<int> Numbers => this.numbers;

        private Answer(int number1, int number2, int number3, int number4)
        {
            this.number1 = number1;
            this.number2 = number2;
            this.number3 = number3;
            this.number4 = number4;

            this.numbers = new List<int> {number1, number2, number3, number4};
        }

        public static Answer Of(int value1, int value2, int value3, int value4)
        {
            Answer answer = new Answer(value1, value2, value3, value4);
            Validate(answer);
            return answer;
        }

        public static Answer Of(IList<int> numbers)
        {
            if (numbers.Count != 4)
            {
                throw new InvalidCountException();
            }

            return Of(numbers[0], numbers[1], numbers[2], numbers[3]);
        }

        private static void Validate(Answer answer)
        {
            ValidateRange(answer);
            ValidateDuplication(answer);
        }

        private static void ValidateDuplication(Answer answer)
        {
            var nums = answer.numbers;
            for (var i = 0; i < nums.Count; i++)
            {
                if (nums.Where((t, j) => j > i).Any(t => nums[i] == t))
                {
                    throw new DuplicatedAnswerException();
                }
            }
        }

        private static void ValidateRange(Answer answer)
        {
            if (answer.numbers.Any(value => value < 0 || value > 9))
            {
                throw new OutOfRangeException();
            }
        }
    }
}