using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GuessNumber.Exceptions;

namespace GuessNumber.Model
{
    public class Answer
    {
        private int _number1;
        private int _number2;
        private int _number3;
        private int _number4;

        public IList<int> Numbers { get; }

        private Answer(int number1, int number2, int number3, int number4)
        {
            _number1 = number1;
            _number2 = number2;
            _number3 = number3;
            _number4 = number4;

            Numbers = new List<int> {number1, number2, number3, number4};

            Validate();
        }

        public static Answer Of(int value1, int value2, int value3, int value4)
        {
            return new Answer(value1, value2, value3, value4);
        }

        public static Answer Of(IList<int> numbers)
        {
            if (numbers.Count != 4)
            {
                throw new InvalidCountException();
            }

            return Of(numbers[0], numbers[1], numbers[2], numbers[3]);
        }

        private void Validate()
        {
            ValidateRange();
            ValidateDuplication();
        }

        private void ValidateDuplication()
        {
            for (var i = 0; i < Numbers.Count; i++)
            {
                if (Numbers.Where((t, j) => j > i).Any(t => Numbers[i] == t))
                {
                    throw new DuplicatedAnswerException();
                }
            }
        }

        private void ValidateRange()
        {
            if (Numbers.Any(value => value < 0 || value > 9))
            {
                throw new OutOfRangeException();
            }
        }
    }
}