using Ludo.Controllers;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.GameModes
{
    public class DefaultMode : IGameMode
    {
        public DefaultMode()
        {
            InputController = new InputController();
        }

        private InputController InputController { get; }

        public void Start(Game game)
        {
            GlobalController.Register(InputController);
        }

        public void Loop(Game game)
        {
            game.CurrentPlayer.ExtraMove = false;

            if (!game.CurrentPlayer.FirstMove)
            {
                game.Status = "Roll the dice";
                game.RefreshUserInterface();
                InputController.Roll(game);

                if (game.Dice.Value == 6) game.CurrentPlayer.ExtraMove = true;
            }
            else
            {
                game.CurrentPlayer.FirstMove = false;

                game.Dice.Set(6);
                game.Status = "Place your first figure";
                game.RefreshUserInterface();
                InputController.PlaceFigure(game);

                game.Status = "Roll the dice";
                game.RefreshUserInterface();
                InputController.Roll(game);
            }

            game.Status = "Move with a figure";
            game.RefreshUserInterface();
            InputController.MovePlayer(game);

            if (!game.CurrentPlayer.ExtraMove) game.NextPlayer();
        }

        public void Dispose(Game game)
        {
            GlobalController.Dispose();
        }
    }
}