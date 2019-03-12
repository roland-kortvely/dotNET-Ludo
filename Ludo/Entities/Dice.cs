using System;

namespace Ludo.Entities
{
    public class Dice
    {
        public Dice()
        {
            Value = 0;
        }

        private static Random Random => new Random();

        public int Value { get; private set; }

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