using Ludo.Models;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            Game.CreateInstance();
            Game.Init();
        }
    }
}