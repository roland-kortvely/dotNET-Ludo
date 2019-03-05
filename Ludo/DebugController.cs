using System;

namespace Ludo
{
    public class DebugController
    {
        private Game Game { get; }

        public DebugController(Game game)
        {
            Game = game;

            game.Status = "DEBUG MODE";
            game.Mode = "| DEBUG";
            game.Draw();
        }

        public void ReadKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    Game.Dice.Roll();
                    Game.Status = "You rolled " + Game.Dice.Value;
                    Game.Draw();
                    break;
                case ConsoleKey.N:
                    Game.NextPlayer();
                    Game.Draw();
                    break;
                case ConsoleKey.D1:
                    Game.Board.MovePlayer(Game, 1);
                    Game.Draw();
                    break;
                case ConsoleKey.D2:
                    Game.Board.MovePlayer(Game, 2);
                    Game.Draw();
                    break;
                case ConsoleKey.D3:
                    Game.Board.MovePlayer(Game, 3);
                    Game.Draw();
                    break;
                case ConsoleKey.D4:
                    Game.Board.MovePlayer(Game, 4);
                    Game.Draw();
                    break;
                case ConsoleKey.S:
                    if (Game.Board.PlayerCanStartWithFigure(Game, Game.CurrentPlayer))
                    {
                        Game.CurrentPlayer.PlaceFigure();
                    }
                    else
                    {
                        Game.Status = "You can't start with a new figure";
                    }
                    Game.Draw();
                    break;
                case ConsoleKey.E:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}
