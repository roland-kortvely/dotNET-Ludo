using System;

namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            var game = new Game(new DefaultBoard());

            game.NewPlayer("Bajka", 'B');
            game.NewNullPlayer();
            game.NewPlayer("Rolko", 'R');
            game.NewNullPlayer();


            game.Dice.Set(6);

            if (game.Board.PlayerCanPlaceFigure(game.Dice, game.Players, game.Player))
            {
                game.Player.PlaceFigure();
            }

            game.NextPlayer();

            if (game.Board.PlayerCanPlaceFigure(game.Dice, game.Players, game.Player))
            {
                game.Player.PlaceFigure();
            }

            game.Draw();

            Input.Listen(new InputController(game));

            while (true)
            {
            }
        }
    }
}