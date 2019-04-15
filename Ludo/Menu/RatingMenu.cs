using System;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Models;

namespace Ludo.Menu
{
    public class RatingMenu : IMenuItem
    {
        public void Render()
        {
            var i = 1;
            foreach (var rating in Game.Instance.RatingService.GetAll())
            {
                Console.Write("#{0} \t {1,-12} \t ", i++, rating.Content);

                for (var stars = 0; stars < rating.Stars; stars++) Console.Write('*');
                Console.WriteLine();
            }

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