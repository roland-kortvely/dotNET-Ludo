using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public abstract class Board : IBoard
    {
        public abstract int MaxPlayers();
        public abstract int PlayerFigures();

        public abstract Cell[,] Map();
        public abstract int[,] Players();
        public abstract int[,] MapIndex();

        public enum Cell
        {
            X, //None
            P, //Path
            H, //Home
            S, //Start                   
        }

        private Figure FigureByPosition(List<Player> players, int position)
        {
            Figure figure = null;

            foreach (var player in players)
            {
                foreach (var current in player.Figures)
                {
                    if (current.Position != position)
                    {
                        continue;
                    }

                    figure = current;
                    break;
                }
            }

            return figure;
        }

        public string Render(List<Player> players)
        {
            var builder = new StringBuilder();

            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    var owner = Players()[i, j];
                    var index = MapIndex()[i, j];

                    var cell = ' ';

                    switch (type)
                    {
                        case Cell.P:

                            Figure figure = FigureByPosition(players, index);
                            if (figure != null)
                            {
                                cell = figure.Player.Symbol;
                            }

                            builder.Append("[" + cell + "]");
                            break;
                        case Cell.S:

                            if (-1 < owner && owner <= players.Count)
                            {
                                cell = players[owner - 1].HasFigureAtStart(index) ? players[owner - 1].Symbol : ' ';
                            }

                            builder.Append("(" + cell + ")");
                            break;
                        case Cell.H:

                            if (-1 < owner && owner <= players.Count)
                            {
                                cell = players[owner - 1].HasFigureAtHome(index) ? players[owner - 1].Symbol : ' ';
                            }

                            builder.Append(" " + cell + " ");
                            break;
                        //case Cell.X:
                        default:
                            builder.Append("   ");
                            break;
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}