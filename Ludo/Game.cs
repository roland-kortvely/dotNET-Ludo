using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public class Game
    {
        public IBoard Board { get; }
        public IGameMode GameMode { get; }

        private IUserInterface UserInterface { get; set; }

        public List<Player> Players { get; }

        public Player CurrentPlayer => Players[_currentPlayer];

        private int _currentPlayer;

        public Dice Dice { get; private set; }

        public string Status { get; set; }
        public string Mode { get; set; }

        public Game(IBoard board, IGameMode gameMode, IUserInterface userInterface)
        {
            Board = board;
            GameMode = gameMode;
            UserInterface = userInterface;

            _currentPlayer = 0;

            Players = new List<Player>();

            Reset();

            Start();
            Loop();
        }

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
            UserInterface.Start(this);
            GameMode.Start(this);
        }

        public void Loop()
        {
            while (!IsGameOver())
            {
                GameMode.Loop(this);
                UserInterface.Loop(this);
            }

            Status = CurrentPlayer.Name + " has won!";

            Reset();
        }

        private void Reset()
        {
            UserInterface.Reset(this);

            Dice = new Dice();

            foreach (var player in Players)
            {
                player.Reset();
            }

            Status = "Game initialized";

            GameMode.Reset(this);
        }

        public bool IsGameOver()
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

        public void RefreshUserInterface()
        {
            UserInterface.Render(this);
        }
    }
}