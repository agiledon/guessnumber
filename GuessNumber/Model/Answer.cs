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
            ValidateNumbersCount(numbers);

            return Of(numbers[0], numbers[1], numbers[2], numbers[3]);
        }

        private static void ValidateNumbersCount(IList<int> numbers)
        {
            if (numbers.Count != 4)
            {
                throw new InvalidAnswerException("The size of answer numbers must be 4.");
            }
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
                    throw new InvalidAnswerException("The numbers of answer can not be duplicated.");
                }
            }
        }

        private void ValidateRange()
        {
            if (Numbers.Any(value => value < 0 || value > 9))
            {
                throw new InvalidAnswerException("The number must be between 0 to 9.");
            }
        }

        public string Matches(Answer inputAnswer)
        {
            var allRight = 0;
            var valueRight = 0;
            foreach (var actualNumber in Numbers)
            {
                foreach (var inputNumber in inputAnswer.Numbers)
                {
                    if (SameValue(inputNumber, actualNumber) && SamePosition(inputAnswer, actualNumber, inputNumber))
                    {
                        allRight++;
                        continue;
                    }

                    if (SameValue(inputNumber, actualNumber))
                    {
                        valueRight++;
                    }
                }
            }

            return $"{allRight}A{valueRight}B";
        }

        private static bool SameValue(int inputNumber, int actualNumber)
        {
            return inputNumber == actualNumber;
        }

        private bool SamePosition(Answer inputAnswer, int actualNumber, int inputNumber)
        {
            return Numbers.IndexOf(actualNumber) == inputAnswer.Numbers.IndexOf(inputNumber);
        }

        public override string ToString()
        {
            return $"{_number1} {_number2} {_number3} {_number4}";
        }
    }
}