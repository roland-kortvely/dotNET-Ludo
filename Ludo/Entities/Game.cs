using System.Collections.Generic;
using Ludo.Database;
using Ludo.Interfaces;
using Ludo.Services;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Entities
{
    public class Game
    {
        private int _currentPlayer;

        public Game(IBoard board, IGameMode gameMode, IUserInterface userInterface)
        {
            Board = board;
            GameMode = gameMode;
            UserInterface = userInterface;

            DB = new LudoContext();

            ScoreService = new ScoreService(this);
            RatingService = new RatingService(this);
            CommentService = new CommentService(this);

            Reset();

            Start();
            Loop();
        }

        public DbContext DB { get; }
        public IBoard Board { get; }
        public IGameMode GameMode { get; }
        public IUserInterface UserInterface { get; }
        public IScoreService ScoreService { get; set; }
        public IRatingService RatingService { get; set; }
        public ICommentService CommentService { get; set; }

        public bool PlayerCanStartWithFigure() => Board.PlayerCanStartWithFigure(this);
        public bool PlayerCanMove(Figure figure) => Board.PlayerCanMove(this, figure);
        public bool MovePlayer(int figureIndex) => Board.MovePlayer(this, figureIndex);
        public bool MovePlayer(Figure figure) => Board.MovePlayer(this, figure);
        public bool MovePossible() => CurrentPlayer.MovePossible(this);
        public bool StartWithFigure() => CurrentPlayer.StartWithFigure();
        public void Roll() => Dice.Roll();

        public List<Player> Players { get; private set; }

        public Player CurrentPlayer => Players[_currentPlayer];

        public Dice Dice { get; private set; }

        public string Status { get; set; }
        public string Mode { get; set; }

        public bool NewPlayer(string name, char symbol)
        {
            if (name == null) return false;

            if (Players.Count + 1 > Board.MaxPlayers()) return false;

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
                    _currentPlayer++;
                else
                    _currentPlayer = 0;

                break;
            }

            Status = CurrentPlayer.Name + "'s turn";
        }

        public void Start()
        {
            GameMode?.Start(this);
            UserInterface?.Start(this);
        }

        public void Loop()
        {
            while (!IsGameOver())
            {
                GameMode?.Loop(this);
                UserInterface?.Loop(this);
            }

            Status = CurrentPlayer.Name + " has won!";

            Reset();
        }

        private void Reset()
        {
            _currentPlayer = 0;

            Players = new List<Player>();
            Dice = new Dice();

            foreach (var player in Players) player.Reset();

            GameMode?.Reset(this);
            UserInterface?.Reset(this);
        }

        public bool IsGameOver()
        {
            foreach (var player in Players)
                if (player.Finished())
                    return true;

            return false;
        }

        public void RefreshUserInterface()
        {
            UserInterface?.Render(this);
        }
    }
}