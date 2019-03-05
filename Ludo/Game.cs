using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public class Game
    {
        public IBoard Board { get; }
        private IGameMode GameMode { get; }

        public List<Player> Players { get; }

        public Player CurrentPlayer => Players[_currentPlayer];

        private int _currentPlayer;

        public Dice Dice { get; private set; }

        public string Status { private get; set; }
        public string Mode { private get; set; }

        public Game(IBoard board, IGameMode gameMode)
        {
            Board = board;
            GameMode = gameMode;

            _currentPlayer = 0;

            Players = new List<Player>();

            Reset();

            Start();
            Loop();
        }

        private bool NewPlayer(string name, char symbol)
        {
            if (name == null)
            {
                return false;
            }

            if (Players.Count + 1 > Board.MaxPlayers())
            {
                return false;
            }

            Players.Add(new Player(Players.Count, name, symbol, Board.PlayerFigures(),
                Board.StartPosition(Players.Count + 1),
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

            Status = CurrentPlayer.Name + "'s turn";
        }

        public void Start()
        {
            var players = 0;

            do
            {
                Console.Write("Player mode (max " + Board.MaxPlayers() + "): ");

                try
                {
                    players = Convert.ToInt32(Console.ReadLine());

                    if (players < 2 || players > Board.MaxPlayers())
                    {
                        Console.WriteLine("Out of range..");
                    }
                }
                catch (Exception e)
                {
                    Status = e.ToString();
                }
            } while (players < 2 || players > Board.MaxPlayers());


            for (var i = 0; i < players; i++)
            {
                Console.Write("Player " + (i + 1) + " name: ");
                var name = Console.ReadLine();

                Console.Write("Player " + (i + 1) + " symbol: ");
                var symbol = Console.Read();
                Console.ReadLine();

                NewPlayer(name, (char) symbol);
            }

            Console.CursorVisible = false;

            if (Players.Count == 0)
            {
                return;
            }

            GameMode.Start(this);
        }

        public void Loop()
        {
            while (!IsGameOver())
            {
                GameMode.Loop(this);
            }

            Status = CurrentPlayer.Name + " has won!";

            //Console.CursorVisible = true;
            Console.ReadKey(true);
        }

        private void Reset()
        {
            Dice = new Dice();

            foreach (var player in Players)
            {
                player.Reset();
            }

            Status = "Game initialized";
        }

        private bool IsGameOver()
        {
            foreach (var player in Players)
            {
                if (player.Finished())
                {
                    return true;
                }
            }

            return false;
        }

        public void Draw()
        {
            if (Players.Count == 0)
            {
                return;
            }

            Console.Clear();

            var builder = new StringBuilder();

            builder.Append("Ludo by Roland Körtvely ").AppendLine(Mode);

            builder.AppendLine("----------------------------");
            
            builder.AppendLine("Use [Space] to roll the dice, [numpad] to move with a figure.");

            builder.Append("Dice: ").Append(Dice.Value.ToString()).Append(" <-> Current player: ")
                .AppendLine(CurrentPlayer.Name);

            builder.AppendLine("Status: " + Status);

            Console.WriteLine(builder.ToString());

            //builder.AppendLine(Board.Render(this));
            Board.Render(this);
        }
    }
}