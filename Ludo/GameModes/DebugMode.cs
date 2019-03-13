using Ludo.Controllers;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.GameModes
{
    public class DebugMode : IGameMode
    {
        public DebugMode(Game game)
        {
            DebugController = new DebugController(game);
        }

        private DebugController DebugController { get; }

        public void Start(Game game)
        {
            game.Dice.Set(6);

            game.Status = "DEBUG MODE";
            game.Mode = "| DEBUG";

            game.RefreshUserInterface();

            GlobalController.Register(DebugController);
        }

        public void Loop(Game game)
        {
        }

        public void Dispose(Game game)
        {
            GlobalController.Dispose();
        }
    }
}