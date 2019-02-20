using System;
using System.Collections.ObjectModel;

namespace Ludo
{
    public class InputController
    {
        private Game Game { get; }

        public InputController(Game game)
        {
            Game = game;
        }

        public void ReadKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Spacebar:
                    Game.Dice.Roll();
                    Game.Draw();
                    break;
                case ConsoleKey.N:
                    Game.NextPlayer();
                    Game.Draw();
                    break;
                case ConsoleKey.M:
                    Game.Board.MovePlayer(Game.Dice, Game.Players, Game.Player);
                    Game.Draw();
                    break;
                case ConsoleKey.S:
                    if (Game.Board.PlayerCanPlaceFigure(Game.Dice, Game.Players, Game.Player))
                    {
                        Game.Player.PlaceFigure();
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