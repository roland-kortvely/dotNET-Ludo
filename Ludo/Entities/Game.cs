using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Menu;
using Ludo.Services;
using Newtonsoft.Json.Linq;

namespace Ludo.Entities
{
    public class Game
    {
        public static Game Instance;

        private int _currentPlayer;

        private Game()
        {
            ScoreService = new ScoreService();
            RatingService = new RatingService();
            CommentService = new CommentService();

            Reset();
        }

        public IBoard Board { get; set; }
        public IGameMode GameMode { private get; set; }
        public IUserInterface UserInterface { private get; set; }
        public IScoreService ScoreService { get; }
        public IRatingService RatingService { get; }
        public ICommentService CommentService { get; }

        public List<Player> Players { get; private set; }

        public Player CurrentPlayer => Players[_currentPlayer];

        public Dice Dice { get; private set; }

        public string Status { get; set; }
        public string Mode { get; set; }

        public JObject ToJson()
        {
            return new JObject
            {
                ["currentPlayer"] = _currentPlayer,
            };
        }

        public static Game GameInstance()
        {
            return Instance ?? (Instance = new Game());
        }

        public static void Init()
        {
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

        public bool PlayerCanStartWithFigure()
        {
            return Board.PlayerCanStartWithFigure(this);
        }

        public bool PlayerCanMove(Figure figure)
        {
            return Board.PlayerCanMove(this, figure);
        }

        public bool MovePlayer(int figureIndex)
        {
            return Board.MovePlayer(this, figureIndex);
        }

        public bool MovePlayer(Figure figure)
        {
            return Board.MovePlayer(this, figure);
        }

        public bool MovePossible()
        {
            return CurrentPlayer.MovePossible(this);
        }

        public bool StartWithFigure()
        {
            return CurrentPlayer.StartWithFigure();
        }

        public void Roll()
        {
            Dice.Roll();
        }

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

            GameMode?.Dispose(this);
            UserInterface?.Reset(this);

            Status = CurrentPlayer.Name + " has won! [Press any key]";

            ScoreService.IncreaseScore(CurrentPlayer.Name);

            Console.ReadKey(true);

            //Comment
            Console.WriteLine("Do you want to add a new Comment? [y|N]");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                try
                {
                    Console.CursorVisible = true;
                    Console.Write("Your comment: ");
                    CommentService.NewComment(CurrentPlayer.Name, Console.ReadLine());
                    Console.CursorVisible = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Rating
            Console.WriteLine("Do you want to add a new Rating? [y|N]");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                try
                {
                    Console.CursorVisible = true;
                    Console.Write("Your rating (0-5): ");
                    var rating = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Your comment: ");
                    RatingService.Rate(rating, Console.ReadLine());
                    Console.CursorVisible = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("[Press any key]");
            Console.ReadKey(true);
        }

        public void Reset()
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