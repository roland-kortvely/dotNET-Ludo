using System.Collections.Generic;

namespace Ludo.Models
{
    public class Player
    {
        public Player(int index, string name, char symbol, int figuresStart, int startPosition,
            int finalPosition)
        {
            Index = index;
            Symbol = symbol;

            Name = name;

            Figures = new List<Figure>();
            for (var i = 0; i < figuresStart; i++) Figures.Add(new Figure(this));

            StartPosition = startPosition;
            FinalPosition = finalPosition;

            FirstMove = true;
            ExtraMove = false;
        }

        public string Name { get; }
        public char Symbol { get; }
        public List<Figure> Figures { get; }

        public bool FirstMove { get; set; }
        public bool ExtraMove { get; set; }

        public int Index { get; }

        public int StartPosition { get; }
        public int FinalPosition { get; }

        public bool HasFigureAtStart(int index = 0)
        {
            if (Figures.Count < index) return false;


            var figure = Figures[index];

            return figure.State == Figure.States.Start;
        }

        public bool HasFigureAtHome(int index)
        {
            if (Figures.Count < index) return false;


            var figure = Figures[index];

            return figure.State == Figure.States.Home;
        }

        public bool StartWithFigure()
        {
            foreach (var figure in Figures)
            {
                if (figure.State != Figure.States.Start) continue;

                figure.PlaceAtStart();
                return true;
            }

            return false;
        }

        public void KickTrigger()
        {
        }

        public void HomeTrigger()
        {
        }

        public bool MovePossible(Game game)
        {
            foreach (var figure in Figures)
                switch (figure.State)
                {
                    case Figure.States.Start:
                        if (game.Board.PlayerCanStartWithFigure(game)) return true;

                        break;
                    case Figure.States.Home:
                    case Figure.States.Playing:
                        if (game.Board.PlayerCanMove(game, figure)) return true;
                        break;
                }

            return false;
        }

        public bool Finished()
        {
            foreach (var figure in Figures)
                if (figure.State != Figure.States.Home)
                    return false;

            return true;
        }

        public void Reset()
        {
            foreach (var figure in Figures) figure.Reset();

            FirstMove = true;
            ExtraMove = false;
        }
    }
}