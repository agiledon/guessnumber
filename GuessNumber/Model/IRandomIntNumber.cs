using System;

namespace GuessNumber.Model
{
    public interface IRandomIntNumber
    {
        int Next();
    }

    public class RandomIntNumber : IRandomIntNumber
    {
        private readonly Random _random = new Random();

        public int Next()
        {
            return _random.Next(0, 9);
        }
    }
}