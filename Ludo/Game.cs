using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public class Game
    {
        private readonly IBoard _board;

        private List<Player> _players;

        private int _currentPlayer;

        public Dice Dice { get; }

        public Game(IBoard board)
        {
            _board = board;
            _currentPlayer = 0;

            _players = new List<Player>();

            Dice = new Dice();
        }

        private Player CurrentPlayer => _players[_currentPlayer];

        public bool NewPlayer(string name, char symbol)
        {
            if (_players.Count + 1 > _board.MaxPlayers())
            {
                return false;
            }

            _players.Add(new Player(name, symbol, _board.PlayerFigures()));

            return true;
        }

        public void Draw()
        {
            var builder = new StringBuilder();

            builder.Append("Dice: ").AppendLine(Dice.Value.ToString());

            builder.Append("Current player: ").AppendLine(CurrentPlayer.Name);

            builder.AppendLine(_board.Render(_players));

            Console.WriteLine(builder.ToString());
        }
    }
}