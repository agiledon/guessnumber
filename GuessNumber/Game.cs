using System.Collections.Generic;
using GuessNumber.Model;

namespace GuessNumber
{
    public class Game
    {
        private readonly int _roundAmount;
        private readonly Round _round;
        private int _guessCount = 0;
        private readonly GuessResult _guessResult;
        private const string CorrectResult = "4A0B";
        private Guess _previousGuess;

        public Game(int roundAmount, AnswerGenerator answerGenerator)
        {
            _roundAmount = roundAmount;
            _round = new Round(answerGenerator.Generate());
            _guessResult = new GuessResult();
        }

        public GuessResult Guess(Answer inputAnswer)
        {
            var result = _round.Guess(inputAnswer);
            _guessCount++;

            if (Win(result))
            {
                ComposeGuessResult(inputAnswer, result, GameResult.Win);
                return _guessResult;
            }

            if (Lose(result))
            {
                ComposeGuessResult(inputAnswer, result, GameResult.Lose);
                return _guessResult;
            }

            ComposeGuessResult(inputAnswer, result, GameResult.TBD);
            return _guessResult;
        }

        private void ComposeGuessResult(Answer inputAnswer, string result, GameResult gameResult)
        {
            _guessResult.CurrentResult = result;
            _guessResult.GameResult = gameResult;
            if (_previousGuess != null)
            {
                _guessResult.AddGuessHistory(_previousGuess);
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