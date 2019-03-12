using System;
using Ludo.Entities;

namespace Ludo.Controllers
{
    public static class InputController
    {
        public static ConsoleKey Read()
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.E) Environment.Exit(1);

            return key;
        }

        public static void Roll(Game game)
        {
            while (Read() != ConsoleKey.Spacebar)
            {
            }

            game.Roll();
//            game.Status = "You rolled " + game.Dice.Value;
            game.RefreshUserInterface();
        }

        public static void MovePlayer(Game game)
        {
            var status = false;
            while (true)
            {
                if (!game.MovePossible())
                {
                    game.Status = "No move possible [Press any key]";
                    game.RefreshUserInterface();

                    Read();
                    break;
                }

                if (status) break;

                switch (Read())
                {
                    case ConsoleKey.D1:
                        status = game.MovePlayer(1);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D2:
                        status = game.MovePlayer(2);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D3:
                        status = game.MovePlayer(3);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D4:
                        status = game.MovePlayer(4);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.S:
                        if (game.PlayerCanStartWithFigure())
                            status = game.StartWithFigure();
                        else
                            game.Status = "You can't start with a new figure";

                        game.RefreshUserInterface();
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
                if (!game.MovePossible())
                {
                    game.Status = "No move possible [Press any key]";
                    game.RefreshUserInterface();
                    break;
                }

                switch (Read())
                {
                    case ConsoleKey.S:
                        if (game.PlayerCanStartWithFigure())
                            game.StartWithFigure();
                        else
                            game.Status = "You can't start with a new figure";

                        game.RefreshUserInterface();
                        break;

                    default:
                        continue;
                }

                break;
            }
        }
    }
}