using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ludo.Controllers;
using Ludo.Interfaces;
using Ludo.Menu;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using LudoLibrary.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ludo.Models
{
    public class Game
    {
        public static Game Instance;

        public int CurrentPlayerIndex { get; set; }

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

        public Player CurrentPlayer => Players[CurrentPlayerIndex];

        public Dice Dice { get; private set; }

        public string Status { get; set; }
        public string Mode { get; set; }

        public JObject ToJson()
        {
            var l = new List<JObject>();
            for (var playerIndex = 0; playerIndex < Players.Count; playerIndex++)
            {
                var player = Players[playerIndex];
                var index = playerIndex;
                l.Add(new JObject
                {
                    ["player"] = index,
                    ["status"] = player.Status
                });
            }

            return new JObject
            {
                ["dice"] = Dice.Value,
                ["currentPlayer"] = CurrentPlayerIndex,
                ["players"] = JsonConvert.SerializeObject(l)
            };
        }

        public static Game CreateInstance()
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

        public bool PlayerCanStartWithFigure(Figure figure)
        {
            return Board.PlayerCanStartWithFigure(this, figure);
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

        public bool FigureKicked(Figure figure)
        {
            return Board.FigureKicked(this, figure);
        }

        public bool MovePossible()
        {
            return CurrentPlayer.MovePossible(this);
        }

        public bool StartWithFigure()
        {
            return CurrentPlayer.StartWithFigure();
        }

        public bool StartWithFigure(Figure figure)
        {
            return CurrentPlayer.StartWithFigure(figure);
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
                if (CurrentPlayerIndex < Players.Count - 1)
                    CurrentPlayerIndex++;
                else
                    CurrentPlayerIndex = 0;

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

            //Rating
            Console.WriteLine("Do you want to add a new Rating? [y|N]");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
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

            Console.WriteLine("[Press any key]");
            Console.ReadKey(true);
        }

        public void Reset()
        {
            Players = new List<Player>();
            Dice = new Dice();

            Dice.Set(6);

            CurrentPlayerIndex = 0;
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