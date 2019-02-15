using System.Collections.Generic;

namespace Ludo
{
    public class Player
    {
        private int _figuresHome;
        private int _figuresStart;

        public string Name { get; }
        public char Symbol { get; }

        public List<Figure> Figures { get; }

        public Player(string name, char symbol, int figuresStart)
        {
            Symbol = symbol;

            Name = name;
            _figuresStart = figuresStart;

            _figuresHome = 0;

            Figures = new List<Figure>();
            for (int i = 0; i < figuresStart; i++)
            {
                Figures.Add(new Figure(this, -1));
            }
        }

        public bool HasFigureAtStart(int index) => _figuresStart >= index + 1;

        public bool HasFigureAtHome(int index) => _figuresHome >= index + 1;
    }
}