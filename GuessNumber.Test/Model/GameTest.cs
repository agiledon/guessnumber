using System.Collections.Generic;
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
            var game = CreateGame();

            var inputAnswer1 = Answer.Of(1, 5, 6, 7);
            var inputAnswer2 = Answer.Of(2, 4, 7, 8);
            var inputAnswer3 = Answer.Of(0, 3, 2, 4);

            var result = game.Guess(inputAnswer1);
            Assert.Equal("1A0B", result.CurrentResult);
            Assert.Equal(GameResult.TBD, result.GameResult);
            Assert.Equal(new List<Guess>(), result.GuessHistory);

            result = game.Guess(inputAnswer2);
            Assert.Equal("0A2B", result.CurrentResult);
            Assert.Equal(GameResult.TBD, result.GameResult);
            Assert.Equal(new List<Guess>() { new Guess(inputAnswer1, "1A0B") }, result.GuessHistory);
            
            result = game.Guess(inputAnswer3);
            Assert.Equal("1A2B", result.CurrentResult);
            Assert.Equal(GameResult.Lose, result.GameResult);
            Assert.Equal(new List<Guess>() { new Guess(inputAnswer1, "1A0B"), new Guess(inputAnswer2, "0A2B") }, result.GuessHistory);
        }

        [Fact]
        public void Should_win_if_first_time_is_right()
        {
            var game = CreateGame();

            var result = game.Guess(Answer.Of(1, 2, 3, 4));

            Assert.Equal(GameResult.Win, result.GameResult);
        }

        [Fact]
        public void Should_win_if_last_time_is_right()
        {
            var game = CreateGame();

            var result = game.Guess(Answer.Of(1, 5, 6, 7));
            Assert.Equal(GameResult.TBD, result.GameResult);

            result = game.Guess(Answer.Of(2, 4, 7, 8));
            Assert.Equal(GameResult.TBD, result.GameResult);

            result = game.Guess(Answer.Of(1, 2, 3, 4));
            Assert.Equal(GameResult.Win, result.GameResult);
        }

        private static Game CreateGame()
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
            return game;
        }
    }
}