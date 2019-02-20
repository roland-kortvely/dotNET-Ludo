using System;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            var game = new Game(new DefaultBoard());
            var players = 0;

            do
            {
                Console.Write("Player mode (max " + game.Board.MaxPlayers() + "): ");

                try
                {
                    players = Convert.ToInt32(Console.ReadLine());

                    if (players < 2 || players > game.Board.MaxPlayers())
                    {
                        Console.WriteLine("Out of range..");
                    }
                }
                catch (Exception e)
                {
//                    Console.WriteLine(e);
//                    throw;
                }
              
            } while (players < 2 || players > game.Board.MaxPlayers());


            for (var i = 0; i < players; i++)
            {
                if (players == 2)
                {
                    game.NewNullPlayer();
                }

                Console.Write("Player " + (i + 1).ToString() + " name: ");
                var name = Console.ReadLine();

                Console.Write(name + " symbol: ");
                var symbol = Console.Read();
                Console.ReadLine();

                game.NewPlayer(name, (char) symbol);
            }
            
            game.Start();
            Game.Run();
        }
    }
}