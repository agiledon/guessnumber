using System.Collections.Generic;
using GuessNumber.Model;

namespace GuessNumber
{
    public class Game
    {
        private readonly int _roundAmount;
        private Answer _actualAnswer;
        private Round _round;
        private int _guessCount = 0;
        private GuessResult _guessResult;

        public Game(int roundAmount, AnswerGenerator answerGenerator)
        {
            _roundAmount = roundAmount;
            _actualAnswer = answerGenerator.Generate();
            _round = new Round(_actualAnswer);
            _guessResult = new GuessResult();
        }

        public GuessResult Guess(Answer inputAnswer)
        {
            var result = _round.Guess(inputAnswer);
            _guessCount++;

            if (result == "4A0B" && _guessCount <= _roundAmount)
            {
                _guessResult.GameResult = GameResult.Win;
            }
            else
            {
                if (_guessCount < _roundAmount)
                {
                    _guessResult.GameResult = GameResult.TBD;
                }
                else
                {
                    _guessResult.GameResult = GameResult.Lose;
                }
            }

            return _guessResult;
        }
    }
}