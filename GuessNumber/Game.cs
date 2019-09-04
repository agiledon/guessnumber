using System.Collections.Generic;
using GuessNumber.Model;

namespace GuessNumber
{
    public class Game
    {
        private readonly int _roundAmount;
        private readonly Round _round;
        private int _guessCount = 0;
        private GuessResult _guessResult;
        private const string CorrectResult = "4A0B";

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
                _guessResult.GameResult = GameResult.Win;
                return _guessResult;
            }

            if (Lose(result))
            {
                _guessResult.GameResult = GameResult.Lose;
                return _guessResult;
            }

            _guessResult.GameResult = GameResult.TBD;
            return _guessResult;
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