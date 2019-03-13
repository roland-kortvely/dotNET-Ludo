using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ludo.Interfaces;

namespace Ludo.Controllers
{
    public static class GlobalController
    {
        private static IMenuItem MenuItem { get; set; }
        private static IController Controller { get; set; }

        static GlobalController()
        {
            Listen();
        }

        public static void Register(IController controller)
        {
            Controller = controller;
        }

        public static void Dispose()
        {
            Controller = null;
        }

        public static void Use(IMenuItem item)
        {
            Console.Clear();

            MenuItem = item;

            Console.CursorVisible = false;

            Console.WriteLine("Ludo by Roland Körtvely ");
            Console.WriteLine("----------------------------");

            item.Render();
        }

        public static void Detach()
        {
            MenuItem = null;
        }

        private static async void Listen()
        {
            while (true)
                await Task.Factory.StartNew(() =>
                {
                    while (!Console.KeyAvailable)
                    {
                    }

                    Call(Console.ReadKey(true).Key);
                });
        }

        private static void Call(ConsoleKey key)
        {
            if (key == ConsoleKey.E)
            {
                Environment.Exit(0);
            }

            try
            {
                Controller?.Process(key);
            }
            catch (Exception e)
            {
            }

            MenuItem?.Process(key);
        }
    }
}