using System;
using System.Collections.Generic;
using System.Linq;

namespace Ludo
{
    public class Player
    {
        private int _figuresHome;
        private int _figuresStart;

        public string Name { get; }
        public char Symbol { get; }

        public List<Figure> Figures { get; }

        public Figure Figure(int index)
        {

            var x = Figures.OrderBy(figure => figure.Position == -1 ? int.MaxValue : figure.Position).ToList();
            
            return x[index - 1];
        }

        public int StartPosition { get; }

        public Player(string name, char symbol, int figuresStart, int startPosition)
        {
            Symbol = symbol;

            Name = name;
            _figuresStart = figuresStart;

            _figuresHome = 0;

            Figures = new List<Figure>();
            for (var i = 0; i < figuresStart; i++)
            {
                Figures.Add(new Figure(this, -1));
            }

            StartPosition = startPosition;
        }

        public bool HasFigureAtStart(int index = -1) => _figuresStart >= index + 1;

        public bool HasFigureAtHome(int index = -1) => _figuresHome >= index + 1;

        public void PlaceFigure()
        {
            if (_figuresStart <= 0)
            {
                Console.WriteLine("Can't place new figure..");
                return;
            }

            foreach (var figure in Figures)
            {
                if (figure.Position != -1)
                {
                    continue;
                }

                figure.PlaceAtStart();
                _figuresStart--;
                break;
            }
        }

       
        //TODO:: remove
        public bool IsNull => Name.Equals("NULL");
    }
}