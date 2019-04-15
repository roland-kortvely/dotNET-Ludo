using System;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Models;

namespace Ludo.Menu
{
    public class CommentsMenu : IMenuItem
    {
        public void Render()
        {
            var i = 1;
            foreach (var comment in Game.Instance.CommentService.GetAll())
                Console.WriteLine("#{0} \t {1,-12} \t {2}", i++, comment.Name, comment.Content);

            Console.WriteLine();
            Console.WriteLine("[C]lear comments table");
            Console.WriteLine("[R]efresh");
            Console.WriteLine("[B]ack");
            Console.WriteLine("[E]xit");
        }

        public void Process(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.C:
                    Game.Instance.CommentService.Clear();
                    GlobalController.Use(new CommentsMenu());
                    break;
                case ConsoleKey.R:
                    GlobalController.Use(new CommentsMenu());
                    break;
                case ConsoleKey.B:
                    GlobalController.Use(new MainMenu());
                    break;
            }
        }
    }
}