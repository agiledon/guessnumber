using System.Collections.Generic;
using GuessNumber.Model;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class RoundTest
    {
        [Theory]
        [MemberData(nameof(GuessRounds))]
        public void Should_guess_numbers(Answer answer, string expectedResult)
        {
            var actualAnswer = Answer.Of(1, 2, 3, 4);
            var round = new Round(actualAnswer);

            var actualResult = round.Guess(answer);

            Assert.Equal(expectedResult, actualResult);
        }

        public static IEnumerable<object[]> GuessRounds()
        {
            yield return new object[] {Answer.Of(1, 5, 6, 7), "1A0B"};
            yield return new object[] {Answer.Of(2, 4, 7, 8), "0A2B"};
            yield return new object[] {Answer.Of(0, 3, 2, 4), "1A2B"};
            yield return new object[] {Answer.Of(5, 6, 7, 8), "0A0B"};
            yield return new object[] {Answer.Of(4, 3, 2, 1), "0A4B"};
            yield return new object[] {Answer.Of(1, 2, 3, 4), "4A0B"};
        }
    }
}