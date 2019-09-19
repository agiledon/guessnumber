using System.Collections.Generic;
using GuessNumber.Model;

namespace GuessNumber
{
    public class Game
    {
        private readonly int _roundAmount;
        private readonly Round _round;
        private int _guessCount = 0;
        private readonly GameResult _gameResult;
        private const string CorrectResult = "4A0B";
        private Guess _previousGuess;

        public Game(int roundAmount, AnswerGenerator answerGenerator)
        {
            _roundAmount = roundAmount;
            _round = new Round(answerGenerator.Generate());
            _gameResult = new GameResult();
        }

        public GameResult Guess(Answer inputAnswer)
        {
            var result = _round.Guess(inputAnswer);
            _guessCount++;

            if (Win(result))
            {
                ComposeGuessResult(inputAnswer, result, GameStatus.Win);
                return _gameResult;
            }

            if (Lose(result))
            {
                ComposeGuessResult(inputAnswer, result, GameStatus.Lose);
                return _gameResult;
            }

            ComposeGuessResult(inputAnswer, result, GameStatus.Continue);
            return _gameResult;
        }

        private void ComposeGuessResult(Answer inputAnswer, string result, GameStatus gameStatus)
        {
            _gameResult.GuessResult = result;
            _gameResult.Status = gameStatus;
            if (_previousGuess != null)
            {
                _gameResult.AddGuessHistory(_previousGuess);
            }
            _previousGuess = new Guess(inputAnswer, result);
        }

        private bool Lose(string result)
        {
            return result != CorrectResult && _guessCount == _roundAmount;
        }

        private bool Win(string result)
        {
            return result == CorrectResult && _guessCount <= _roundAmount;
        }
    }
}