using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public abstract class Board : IBoard
    {
        public abstract int MaxPlayers();
        public abstract int PlayerFigures();
        public abstract int Size();

        public abstract Cell[,] Map();
        public abstract int[,] Players();
        public abstract int[,] MapIndex();

        public enum Cell
        {
            _, //None
            R, //Road
            H, //Home
            P, //Player
            S, //Start
        }

        public int StartPosition(int index)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    var owner = Players()[i, j];
                    var mapIndex = MapIndex()[i, j];

                    switch (type)
                    {
                        case Cell.S:
                            if (owner == index)
                            {
                                return mapIndex;
                            }

                            break;
                    }
                }
            }

            return -1;
        }

        private int Transform(int position) => position >= Size() ? position - Size() : position;

        private static Figure FigureByPosition(IEnumerable<Player> players, int position)
        {
            if (players == null)
            {
                return null;
            }

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

        private static bool CanPlaceFigureAtStart(IEnumerable<Player> players, Player player)
        {
            if (players == null)
            {
                return false;
            }

            var figure = FigureByPosition(players, player.StartPosition);
            return figure == null || figure.Player != player;
        }

        public bool PlayerCanPlaceFigure(Dice dice, List<Player> players, Player player)
        {      
            return dice.Value == 6 && player.HasFigureAtHome() && CanPlaceFigureAtStart(players, player);
        }

        public bool PlayerCanMove(Game game, Figure figure)
        {
            if (figure.Position == -1)
            {
                game.Status = "You cannot move with this figure";
                return false;
            }
            
            var position = Transform(figure.Position + game.Dice.Value);
            
            var cell = FigureByPosition(game.Players, position);
            if (cell != null && figure.Player == cell.Player)
            {
                game.Status = "You cannot move with this figure";
                return false;
            }

            return true;
        }

        public void MovePlayer(Game game, Figure figure)
        {
            if (!PlayerCanMove(game, figure))
            {
                return;
            }
            
            var position = Transform(figure.Position + game.Dice.Value);
         
            figure.NewPosition(position);         
        }

        public string Render(List<Player> players)
        {
            if (players == null)
            {
                return null;
            }

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
                        case Cell.S:
                        case Cell.R:

                            if (type == Cell.S)
                            {
                                cell = '*';
                            }

                            var figure = FigureByPosition(players, index);
                            if (figure != null)
                            {
                                cell = figure.Player.Symbol;
                            }

                            builder.Append("[" + cell + "]");
                            break;
                        case Cell.P:

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