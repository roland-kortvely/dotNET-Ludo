using System;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            Console.WriteLine("Ready?");
            
            if (Console.ReadKey(true).Key == ConsoleKey.D)
            {
                new Game(new DefaultBoard(), new DebugMode());
            }
            else
            {
                new Game(new DefaultBoard(), new DefaultMode());
            }
        }
    }
}