using System;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.Controllers
{
    public class InputController : IController
    {
        private ConsoleKey _key;
        private bool _block = true;

        public void Process(ConsoleKey key)
        {
            _key = key;
            _block = false;
        }

        private ConsoleKey Read()
        {
            while (_block)
            {
            }

            _block = true;
            return _key;
        }

        public void Roll(Game game)
        {
            while (Read() != ConsoleKey.Spacebar)
            {
            }

            game.Roll();
            game.RefreshUserInterface();
        }

        public void MovePlayer(Game game)
        {
            var status = false;
            while (true)
            {
                if (!game.MovePossible())
                {
                    game.Status = "No move possible [Press any key]";
                    game.RefreshUserInterface();

                    Read();
                    break;
                }

                if (status) break;

                switch (Read())
                {
                    case ConsoleKey.D1:
                        status = game.MovePlayer(1);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D2:
                        status = game.MovePlayer(2);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D3:
                        status = game.MovePlayer(3);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.D4:
                        status = game.MovePlayer(4);
                        game.RefreshUserInterface();
                        continue;
                    case ConsoleKey.S:
                        if (game.PlayerCanStartWithFigure())
                            status = game.StartWithFigure();
                        else
                            game.Status = "You can't start with a new figure";

                        game.RefreshUserInterface();
                        continue;

                    default:
                        continue;
                }
            }
        }

        public void PlaceFigure(Game game)
        {
            while (true)
            {
                if (!game.MovePossible())
                {
                    game.Status = "No move possible [Press any key]";
                    game.RefreshUserInterface();
                    break;
                }

                switch (Read())
                {
                    case ConsoleKey.S:
                        if (game.PlayerCanStartWithFigure())
                            game.StartWithFigure();
                        else
                            game.Status = "You can't start with a new figure";

                        game.RefreshUserInterface();
                        break;

                    default:
                        continue;
                }

                break;
            }
        }
    }
}