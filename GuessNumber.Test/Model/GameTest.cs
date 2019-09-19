using System.Collections.Generic;
using GuessNumber.Model;
using Moq;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class GameTest
    {
        private readonly Game _game = CreateGame();

        [Fact]
        public void Should_lose_if_all_3_times_are_wrong()
        {
            var inputAnswer1 = Answer.Of(1, 5, 6, 7);
            var result = _game.Guess(inputAnswer1);
            var history = new List<Guess>();
            AssertGuessResult(result, "1A0B", GameStatus.Continue, history);

            var inputAnswer2 = Answer.Of(2, 4, 7, 8);
            result = _game.Guess(inputAnswer2);
            history.Add(new Guess(inputAnswer1, "1A0B"));
            AssertGuessResult(result, "0A2B", GameStatus.Continue, history);

            var inputAnswer3 = Answer.Of(0, 3, 2, 4);
            result = _game.Guess(inputAnswer3);
            history.Add(new Guess(inputAnswer2, "0A2B"));
            AssertGuessResult(result, "1A2B", GameStatus.Lose, history);
        }

        [Fact]
        public void Should_win_if_first_time_is_right()
        {
            var result = _game.Guess(Answer.Of(1, 2, 3, 4));

            AssertGuessResult(result, "4A0B", GameStatus.Win, new List<Guess>());
        }

        [Fact]
        public void Should_win_if_last_time_is_right()
        {
            var inputAnswer1 = Answer.Of(1, 5, 6, 7);
            var result = _game.Guess(inputAnswer1);
            var history = new List<Guess>();
            AssertGuessResult(result, "1A0B", GameStatus.Continue, history);

            var inputAnswer2 = Answer.Of(2, 4, 7, 8);
            result = _game.Guess(inputAnswer2);
            history.Add(new Guess(inputAnswer1, "1A0B"));
            AssertGuessResult(result, "0A2B", GameStatus.Continue, history);

            var inputAnswer3 = Answer.Of(1, 2, 3, 4);
            result = _game.Guess(inputAnswer3);
            history.Add(new Guess(inputAnswer2, "0A2B"));
            AssertGuessResult(result, "4A0B", GameStatus.Win, history);
        }

        private static void AssertGuessResult(GameResult result, string currentResult, GameStatus gameStatus,
            List<Guess> history)
        {
            Assert.Equal(currentResult, result.GuessResult);
            Assert.Equal(gameStatus, result.Status);
            Assert.Equal(history, result.GuessHistory);
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