using System;

namespace Ludo
{
    public class Dice
    {
        private readonly Random _random = new Random();

        public int Value { get; private set; }

        public Dice()
        {
            Value = 0;
        }

        public int Roll()
        {
            var val = _random.Next(6) + 1;
            Value = val;
            return val;
        }
    }
}