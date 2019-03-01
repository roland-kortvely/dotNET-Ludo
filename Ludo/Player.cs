using System.Collections.Generic;
using System.Linq;

namespace Ludo
{
    public class Player
    {
        private int FiguresHome { get; set; }
        private int FiguresStart { get; set; }

        public string Name { get; }
        public char Symbol { get; }

        public List<Figure> Figures { get; }

        public Figure FigureByPosition(int index, int mapSize)
        {
            var x = new List<Figure>();
            foreach (var figure in Figures)
            {
                if (figure.State != Figure.States.Start)
                {
                    x.Add(figure);
                }
            }

            x = x.OrderBy(figure1 =>
            {
                var fig1 = figure1.Position;
                if (figure1.State == Figure.States.Home)
                {
                    return fig1 + mapSize;
                }
                
                return fig1 >= figure1.Player.StartPosition
                    ? fig1 - figure1.Player.StartPosition
                    : fig1 + (mapSize - figure1.Player.StartPosition);
            }).ToList();
        

            //DEBUG indexing on board
            for (var i = 0; i < x.Count; i++)
            {
                x[i].Index = i + 1;
            }   

            return x.Count >= index ? x[index - 1] : null;
        }

        public int StartPosition { get; }
        public int FinalPosition { get; }

        public Player(string name, char symbol, int figuresStart, int startPosition, int finalPosition)
        {
            Symbol = symbol;

            Name = name;
            FiguresStart = figuresStart;

            FiguresHome = 0;

            Figures = new List<Figure>();
            for (var i = 0; i < figuresStart; i++)
            {
                Figures.Add(new Figure(this, -1));
            }

            StartPosition = startPosition;
            FinalPosition = finalPosition;
        }

        public bool HasFigureAtStart(int index = -1) => FiguresStart >= index + 1;

        public bool HasFigureAtHome(int index)
        {
            foreach (var figure in Figures)
            {
                if (figure.State != Figure.States.Home)
                {
                    continue;
                }

                if (figure.Position == index)
                {
                    return true;
                }
            }

            return false;
        }

        public void PlaceFigure()
        {
            if (FiguresStart <= 0)
            {
                return;
            }

            foreach (var figure in Figures)
            {
                if (figure.State != Figure.States.Start)
                {
                    continue;
                }

                figure.PlaceAtStart();
                FiguresStart--;
                break;
            }
        }

        public void KickTrigger()
        {
            FiguresStart++;
        }

        public void HomeTrigger()
        {
            FiguresHome++;
        }
    }
}