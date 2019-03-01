using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Ludo
{
    public class Game
    {
        public IBoard Board { get; }

        public List<Player> Players { get; }

        public Player Player => Players[_currentPlayer];

        private int _currentPlayer;

        public Dice Dice { get; private set; }

        public string Status { private get; set; }

        public Game(IBoard board)
        {
            Board = board;
            _currentPlayer = 0;

            Players = new List<Player>();

            Reset();
        }

        public Player CurrentPlayer => Players[_currentPlayer];

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

            Players.Add(new Player(name, symbol, Board.PlayerFigures(), Board.StartPosition(Players.Count + 1),
                Board.FinalPosition(Players.Count + 1)));

            return true;
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

                break;
            }

            Status = "Player " + CurrentPlayer.Name + " turn";
        }

        public void Start()
        {
            Console.CursorVisible = false;

            if (Players.Count == 0)
            {
                return;
            }

            Input.Listen(new InputController(this));

            Status = "Player " + CurrentPlayer.Name + " turn";

            Draw();
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(100);
            }

            Console.ReadKey();
        }

        public void Reset()
        {
            Dice = new Dice();
            Status = "Game initialized";
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

            builder.Append("Status: ").AppendLine(Status);

            Console.WriteLine(builder.ToString());

            Status = "";
        }
    }
}