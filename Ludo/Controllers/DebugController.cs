using System;
using Ludo.Entities;

namespace Ludo.Controllers
{
    public class DebugController
    {
        public DebugController(Game game)
        {
            Game = game;

            game.Status = "DEBUG MODE";
            game.Mode = "| DEBUG";
            game.RefreshUserInterface();
        }

        private Game Game { get; }

        public void ReadKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:

                    foreach (var figure in Game.CurrentPlayer.Figures)
                    {
                        figure.Home();
                    }

                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.Spacebar:
                    Game.Roll();
                    Game.Status = "You rolled " + Game.Dice.Value;
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.N:
                    Game.NextPlayer();
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.D1:
                    Game.MovePlayer(1);
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.D2:
                    Game.MovePlayer(2);
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.D3:
                    Game.MovePlayer(3);
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.D4:
                    Game.MovePlayer(4);
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.S:
                    if (Game.PlayerCanStartWithFigure())
                        Game.StartWithFigure();
                    else
                        Game.Status = "You can't start with a new figure";
                    Game.RefreshUserInterface();
                    break;
                case ConsoleKey.E:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}