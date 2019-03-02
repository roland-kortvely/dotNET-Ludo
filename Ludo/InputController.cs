using System;

namespace Ludo
{
    public static class InputController
    {
        private static ConsoleKey Read()
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.E)
            {
                Environment.Exit(1);
            }

            return key;
        }

        public static void Roll(Game game)
        {
            while (Read() != ConsoleKey.Spacebar)
            {
            }

            game.Dice.Roll();
            game.Status = "You rolled " + game.Dice.Value;
            game.Draw();
        }

        public static void MovePlayer(Game game)
        {
            var status = false;
            while (true)
            {
                if (!game.CurrentPlayer.MovePossible(game))
                {
                    game.Status = "No move possible";
                    game.Draw();

                    Read();
                    break;
                }
                
                if (status)
                {
                    break;
                }

                switch (Read())
                {
                    case ConsoleKey.D1:
                        status = game.Board.MovePlayer(game, 1);
                        game.Draw();
                        continue;
                    case ConsoleKey.D2:
                        status = game.Board.MovePlayer(game, 2);
                        game.Draw();
                        continue;
                    case ConsoleKey.D3:
                        status = game.Board.MovePlayer(game, 3);
                        game.Draw();
                        continue;
                    case ConsoleKey.D4:
                        status = game.Board.MovePlayer(game, 4);
                        game.Draw();
                        continue;
                    case ConsoleKey.S:
                        if (game.Board.PlayerCanPlaceFigure(game, game.CurrentPlayer))
                        {
                            status = game.CurrentPlayer.PlaceFigure();
                        }
                        else
                        {
                            game.Status = "You can't start with a new figure";
                        }

                        game.Draw();
                        continue;

                    default:
                        continue;
                }
            }
        }

        public static void PlaceFigure(Game game)
        {
            while (true)
            {
                if (!game.CurrentPlayer.MovePossible(game))
                {
                    game.Status = "No move possible";
                    game.Draw();
                    break;
                }
                
                switch (Read())
                {                  
                    case ConsoleKey.S:
                        if (game.Board.PlayerCanPlaceFigure(game, game.CurrentPlayer))
                        {
                            game.CurrentPlayer.PlaceFigure();
                        }
                        else
                        {
                            game.Status = "You can't start with a new figure";
                        }

                        game.Draw();
                        break;

                    default:
                        continue;
                }

                break;
            }
        }
    }
}