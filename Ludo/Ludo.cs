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
            game.NextPlayer();
            
            if (game.Board.PlayerCanPlaceFigure(game.Dice, game.Players, game.Player))
            {
                game.Player.PlaceFigure();
            }
            game.NextPlayer();
            
            if (game.Board.PlayerCanPlaceFigure(game.Dice, game.Players, game.Player))
            {
                game.Player.PlaceFigure();
            }
            game.NextPlayer();


            game.Dice.Roll();
            
           // Console.WriteLine("Can move " + game.Player.Name + " " + game.Board.PlayerCanMove(game.Dice, game.Players, game.Player));
           game.Board.MovePlayer(game.Dice, game.Players, game.Player);
           game.NextPlayer();
           game.Board.MovePlayer(game.Dice, game.Players, game.Player);
          
            
            game.Draw();
        }
    }
}