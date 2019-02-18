using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public class Game
    {
        public IBoard Board { get; }

        public List<Player> Players { get; }

        public Player Player => Players[_currentPlayer];

        private int _currentPlayer;

        public Dice Dice { get; }

        public Game(IBoard board)
        {
            Board = board;
            _currentPlayer = 0;

            Players = new List<Player>();

            Dice = new Dice();
        }

        private Player CurrentPlayer => Players[_currentPlayer];

        public bool NewPlayer(string name, char symbol)
        {
            if (name == null)
            {
                return false;
            }

            if (Players.Count + 1 > Board.MaxPlayers())
            {
                return false;
            }

            Players.Add(new Player(name, symbol, Board.PlayerFigures(), Board.StartPosition(Players.Count)));

            return true;
        }

        /**
         * Used in case of One-to-One game
         */
        public bool NewNullPlayer()
        {
            return NewPlayer("NULL", ' ');
        }

        public void NextPlayer()
        {
            while (true)
            {
                if (_currentPlayer < Players.Count - 1)
                {
                    _currentPlayer++;
                }
                else
                {
                    _currentPlayer = 0;
                }

                if (Player.IsNull && Players.Count > 1)
                {
                    continue;
                }

                break;
            }
        }

        public void Start()
        {
            if (Players.Count == 0)
            {
                return;
            }

            if (CurrentPlayer.IsNull)
            {
                NextPlayer();
            }

            Input.Listen(new InputController(this));

            Draw();
        }

        public void Run()
        {
            while (true)
            {
            }
        }

        public void Draw()
        {
            if (Players.Count == 0)
            {
                return;
            }

            Console.Clear();

            var builder = new StringBuilder();

            builder.Append("Dice: ").AppendLine(Dice.Value.ToString());

            builder.Append("Current player: ").AppendLine(CurrentPlayer.Name);

            builder.AppendLine(Board.Render(Players));

            Console.WriteLine(builder.ToString());
        }
    }
}