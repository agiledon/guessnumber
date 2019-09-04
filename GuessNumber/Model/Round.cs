namespace GuessNumber.Model
{
    public class Round
    {
        private readonly Answer _actualAnswer;

        public Round(Answer actualAnswer)
        {
            _actualAnswer = actualAnswer;
        }

        public string Guess(Answer inputAnswer)
        {
            return _actualAnswer.Matches(inputAnswer);
        }
    }
}