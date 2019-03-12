using System;
using System.Threading.Tasks;
using Ludo.Controllers;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.GameModes
{
    public class DebugMode : IGameMode
    {
        private static DebugController _controller;

        public void Start(Game game)
        {
            Listen(new DebugController(game));

            game.Dice.Set(6);

            game.RefreshUserInterface();
        }

        public void Loop(Game game)
        {
        }

        public void Reset(Game game)
        {
        }

        private static async void Listen(DebugController controller)
        {
            _controller = controller;

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
            _controller?.ReadKey(key);
        }
    }
}