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
            Start();
        }

        private static void Start()
        {
            Console.CursorVisible = false;

            Console.WriteLine("Ludo by Roland Körtvely ");
            Console.WriteLine("----------------------------");

            Console.WriteLine("Start a new Game:");
            Console.WriteLine("[D]ebug");
            Console.WriteLine("[B]ot");
            Console.WriteLine("[N]ormal");

            Console.WriteLine();

            Console.WriteLine("Other options:");
            Console.WriteLine("[E]xit");

            var key = Console.ReadKey(true).Key;

            Console.Clear();
            
            switch (key)
            {
                case ConsoleKey.D:
                    Console.WriteLine("You selected Debug Mode");
                    new Game(new DefaultBoard(), new DebugMode(), new ConsoleUI());
                    break;
                case ConsoleKey.B:
                    Console.WriteLine("You selected BOT Mode");
                    new Game(new DefaultBoard(), new BotMode(), new ConsoleUI());
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("You selected Default Mode");
                    new Game(new DefaultBoard(), new DefaultMode(), new ConsoleUI());
                    break;
                case ConsoleKey.E:
                    Environment.Exit(0);
                    break;
                default:
                    Start();
                    break;
            }
        }
    }
}