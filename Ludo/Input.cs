using System;
using System.Threading.Tasks;

namespace Ludo
{
    public static class Input
    {
        private static InputController _controller;

        public static void Listen(InputController controller)
        {
            _controller = controller;

            Listen();
        }

        private static async void Listen()
        {
            while (true)
            {
                await Task.Factory.StartNew(() =>
                {
                    while (!Console.KeyAvailable)
                    {
                    }

                    Call(Console.ReadKey(true).Key);
                });
            }
        }

        private static void Call(ConsoleKey key)
        {
            _controller?.ReadKey(key);
        }
    }
}