using System;

namespace Ludo
{
    public class Dice
    {
        private static Random Random => new Random();

        public int Value { get; private set; }

        public Dice()
        {
            Value = 0;
        }

        public int Roll()
        {
            var val = Random.Next(6) + 1;
            Value = val;
            return val;
        }

        public void Set(int value)
        {
            Value = value;
        }
    }
}