namespace Ludo
{
    public class DefaultMode : IGameMode
    {
        public void Start(Game game)
        {
        }

        public void Loop(Game game)
        {
            game.CurrentPlayer.ExtraMove = false;

            if (!game.CurrentPlayer.FirstMove)
            {
                game.Status = "Roll the dice";
                game.Draw();
                InputController.Roll(game);

                if (game.Dice.Value == 6)
                {
                    game.CurrentPlayer.ExtraMove = true;
                }
            }
            else
            {
                game.CurrentPlayer.FirstMove = false;

                game.Dice.Set(6);
                game.Status = "Place your first figure";
                game.Draw();
                InputController.PlaceFigure(game);

                game.Status = "Roll the dice";
                game.Draw();
                InputController.Roll(game);
            }

            game.Status = "Move with figure";
            game.Draw();
            InputController.MovePlayer(game);

            if (!game.CurrentPlayer.ExtraMove)
            {
                game.NextPlayer();
            }
        }
    }
}