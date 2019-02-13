using System;

namespace Ludo
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new DefaultBoard());
            
            game.Draw();
        }
    }
}