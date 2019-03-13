using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ludo.Boards;
using Ludo.Controllers;
using Ludo.Database;
using Ludo.GameModes;
using Ludo.Interfaces;
using Ludo.Menu;
using Ludo.Services;
using Ludo.UserInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Entities
{
    public class Game
    {
        public static Game Instance;

        private int _currentPlayer;

        private Game()
        {
            DB = new LudoContext();

            ScoreService = new ScoreService();
            RatingService = new RatingService();
            CommentService = new CommentService();

            Reset();
        }

        public static void GameInstance()
        {
            Instance = new Game();

            GlobalController.Use(new MainMenu());

            while (true)
            {
            }
        }

        public async void Run()
        {
            Console.Clear();

            await Task.Factory.StartNew(() =>
            {
                Start();
                Loop();
                Reset();

                GlobalController.Use(new MainMenu());
            });
        }

        public LudoContext DB { get; }
        public IBoard Board { get; set; }
        public IGameMode GameMode { private get; set; }
        public IUserInterface UserInterface { private get; set; }
        public IScoreService ScoreService { get; }
        public IRatingService RatingService { get; }
        public ICommentService CommentService { get; }

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

        private void Start()
        {
            UserInterface?.Start(this);
            GameMode?.Start(this);
        }

        private void Loop()
        {
            while (!IsGameOver())
            {
                GameMode?.Loop(this);
                UserInterface?.Loop(this);
            }


            Status = CurrentPlayer.Name + " has won! [Press any key]";

            var score = ScoreService.Get(CurrentPlayer.Name);
            if (score != null)
            {
                score.Points += 10;
                ScoreService.Save();
            }
            else
            {
                ScoreService.Add(new Score {Name = CurrentPlayer.Name, Points = 10});
            }

            GameMode?.Reset(this);
            UserInterface?.Reset(this);

            Console.ReadKey(true);
        }

        private void Reset()
        {
            _currentPlayer = 0;

            Players = new List<Player>();
            Dice = new Dice();
        }

        private bool IsGameOver()
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