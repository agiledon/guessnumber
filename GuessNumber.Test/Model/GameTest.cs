using GuessNumber.Model;
using Moq;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class GameTest
    {
        [Fact]
        public void Should_lose_if_all_3_times_are_wrong()
        {
            var mockRandom = new Mock<IRandomIntNumber>();
            mockRandom.SetupSequence(r => r.Next())
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4);
            var answerGenerator = new AnswerGenerator(mockRandom.Object);
            const int roundAmount = 3;

            var game = new Game(roundAmount, answerGenerator);

            var result = game.Guess(Answer.Of(1, 5, 6, 7));
            Assert.Equal(GameResult.TBD, result.GameResult);

            result = game.Guess(Answer.Of(2, 4, 7, 8));
            Assert.Equal(GameResult.TBD, result.GameResult);

            result = game.Guess(Answer.Of(0, 3, 2, 4));
            Assert.Equal(GameResult.Lose, result.GameResult);
        } 
    }
}