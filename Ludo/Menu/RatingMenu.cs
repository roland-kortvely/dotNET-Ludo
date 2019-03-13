using System;
using Ludo.Controllers;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.Menu
{
    public class RatingMenu : IMenuItem
    {
        public void Render()
        {
            var i = 1;
            foreach (Rating rating in Game.Instance.RatingService.GetAll())
                Console.WriteLine("#{0} \t {1,-12} \t {2}", i++, rating.Content, rating.Stars);

            Console.WriteLine();
            Console.WriteLine("[C]lear ratings table");
            Console.WriteLine("[R]efresh");
            Console.WriteLine("[B]ack");
            Console.WriteLine("[E]xit");
        }

        public void Process(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.C:
                    Game.Instance.RatingService.Clear();
                    GlobalController.Use(new RatingMenu());
                    break;
                case ConsoleKey.R:
                    GlobalController.Use(new RatingMenu());
                    break;
                case ConsoleKey.B:
                    GlobalController.Use(new MainMenu());
                    break;
            }
        }
    }
}