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
                    Game.Status = "You rolled " + Game.Dice.Value;
                    Game.Draw();
                    break;
                case ConsoleKey.N:
                    Game.NextPlayer();
                    Game.Draw();
                    break;
                case ConsoleKey.D1:
                    Game.Board.MovePlayer(Game, Game.Player.Figure(1));
                    Game.Draw();
                    break;
                case ConsoleKey.D2:
                    Game.Board.MovePlayer(Game, Game.Player.Figure(2));
                    Game.Draw();
                    break;
                case ConsoleKey.D3:
                    Game.Board.MovePlayer(Game, Game.Player.Figure(3));
                    Game.Draw();
                    break;
                case ConsoleKey.D4:
                    Game.Board.MovePlayer(Game, Game.Player.Figure(4));
                    Game.Draw();
                    break;
                case ConsoleKey.S:
                    if (Game.Board.PlayerCanPlaceFigure(Game.Dice, Game.Players, Game.Player))
                    {
                        Game.Player.PlaceFigure();
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