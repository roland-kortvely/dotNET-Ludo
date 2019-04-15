using System;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Models;

namespace Ludo.Menu
{
    public class ScoreMenu : IMenuItem
    {
        public void Render()
        {
            var i = 1;
            foreach (var score in Game.Instance.ScoreService.GetAll())
                Console.WriteLine("#{0} \t {1,-12} \t {2}", i++, score.Name, score.Points);

            Console.WriteLine();
            Console.WriteLine("[C]lear score table");
            Console.WriteLine("[R]efresh");
            Console.WriteLine("[B]ack");
            Console.WriteLine("[E]xit");
        }

        public void Process(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.C:
                    Game.Instance.ScoreService.Clear();
                    GlobalController.Use(new ScoreMenu());
                    break;
                case ConsoleKey.R:
                    GlobalController.Use(new ScoreMenu());
                    break;
                case ConsoleKey.B:
                    GlobalController.Use(new MainMenu());
                    break;
            }
        }
    }
}