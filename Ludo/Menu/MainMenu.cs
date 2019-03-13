using System;
using Ludo.Boards;
using Ludo.Controllers;
using Ludo.Entities;
using Ludo.GameModes;
using Ludo.Interfaces;
using Ludo.UserInterfaces;

namespace Ludo.Menu
{
    public class MainMenu : IMenuItem
    {
        public void Render()
        {
            var top = Game.Instance.ScoreService.GetTop();
            if (top.Count > 0)
            {
                Console.WriteLine("Top 3 players:");
                var i = 1;
                foreach (Score score in top) Console.WriteLine("#{0} \t {1,-12} \t {2}", i++, score.Name, score.Points);

                Console.WriteLine();
            }

            Console.WriteLine("Start a new Game:");
            Console.WriteLine("[N]ormal");
            Console.WriteLine("[B]ot");
            Console.WriteLine("[D]ebug");

            Console.WriteLine();

            Console.WriteLine("Other options:");
            Console.WriteLine("[S]core");
            Console.WriteLine("[R]atings");
            Console.WriteLine("[C]omments");
            Console.WriteLine("[E]xit");
        }

        public void Process(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D:
                    GlobalController.Detach();
                    Console.WriteLine("You selected Debug Mode");
                    Game.Instance.Board = new DefaultBoard();
                    Game.Instance.GameMode = new DebugMode(Game.Instance);
                    Game.Instance.UserInterface = new ConsoleUI();
                    Game.Instance.Run();
                    break;
                case ConsoleKey.B:
                    GlobalController.Detach();
                    Console.WriteLine("You selected BOT Mode");
                    Game.Instance.Board = new DefaultBoard();
                    Game.Instance.GameMode = new BotMode();
                    Game.Instance.UserInterface = new ConsoleUI();
                    Game.Instance.Run();
                    break;
                case ConsoleKey.N:
                    GlobalController.Detach();
                    Console.WriteLine("You selected Default Mode");
                    Game.Instance.Board = new DefaultBoard();
                    Game.Instance.GameMode = new DefaultMode();
                    Game.Instance.UserInterface = new ConsoleUI();
                    Game.Instance.Run();
                    break;

                case ConsoleKey.S:
                    GlobalController.Use(new ScoreMenu());
                    break;

                case ConsoleKey.R:
                    GlobalController.Use(new RatingMenu());
                    break;

                case ConsoleKey.C:
                    GlobalController.Use(new CommentsMenu());
                    break;
            }
        }
    }
}