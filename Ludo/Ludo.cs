using System;
using Ludo.Boards;
using Ludo.Entities;
using Ludo.GameModes;
using Ludo.UserInterfaces;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            Console.WriteLine("Press any key to start..");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D:
                    new Game(new DefaultBoard(), new DebugMode(), new ConsoleUI());
                    break;
                case ConsoleKey.B:
                    new Game(new DefaultBoard(), new BotMode(), new ConsoleUI());
                    break;
                default:
                    new Game(new DefaultBoard(), new DefaultMode(), new ConsoleUI());
                    break;
            }
        }
    }
}