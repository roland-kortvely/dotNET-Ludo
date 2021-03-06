using System.Threading;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Models;

namespace Ludo.GameModes
{
    public class BotMode : IGameMode
    {
        private Game _game;

        public BotMode()
        {
            InputController = new InputController();
        }

        private InputController InputController { get; }

        public void Start(Game game)
        {
            _game = game;
            game.Mode = "| AGAINST BOT";

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

            if (!game.CurrentPlayer.ExtraMove)
            {
                game.NextPlayer();

                //BOT
                game.Status = "BOT's turn";

                WaitAndRender(300);

                //--First Move
                if (game.CurrentPlayer.FirstMove)
                    BotStart(game);
                else
                    BotTurn(game);

                // Return to human player
                game.NextPlayer();
            }
        }

        public void Dispose(Game game)
        {
            GlobalController.Dispose();
        }

        private void WaitAndRender(int sleep = 500)
        {
            _game.RefreshUserInterface();
            Thread.Sleep(sleep);
        }

        private void BotStart(Game game)
        {
            game.CurrentPlayer.FirstMove = false;
            game.Dice.Set(6);

            WaitAndRender();

            game.StartWithFigure();

            WaitAndRender();

            game.Roll();

            WaitAndRender();

            game.MovePlayer(1);

            WaitAndRender();
        }

        private void BotTurn(Game game)
        {
            do
            {
                WaitAndRender();

                game.Roll();

                WaitAndRender();

                if (!game.MovePossible()) return;

                if (game.PlayerCanStartWithFigure())
                {
                    game.StartWithFigure();
                    WaitAndRender();
                    continue;
                }

                foreach (var figure in game.CurrentPlayer.Figures)
                {
                    if (figure.State != Figure.States.Playing) continue;

                    if (!game.PlayerCanMove(figure)) continue;

                    if (game.MovePlayer(figure)) break;
                }
            } while (game.Dice.Value == 6);
        }
    }
}