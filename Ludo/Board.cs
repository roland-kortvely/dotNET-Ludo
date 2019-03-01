using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo
{
    public abstract class Board : IBoard
    {
        public abstract int MaxPlayers();
        public abstract int PlayerFigures();

        protected abstract int Size();
        protected abstract Cell[,] Map();
        protected abstract int[,] Players();
        protected abstract int[,] MapIndex();

        public enum Cell
        {
            _, //None
            R, //Road
            H, //Home
            P, //Player
            S, //Start
            F
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

        public int FinalPosition(int index)
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
                        case Cell.F:
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

        private int Transform(int position)
        {
            return position >= Size() ? position - Size() : position;
        }

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

                    if (current.State != Figure.States.Playing)
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
            return dice.Value == 6 && player.HasFigureAtStart() && CanPlaceFigureAtStart(players, player);
        }

        public bool PlayerCanMove(Game game, Figure figure)
        {
            if (figure == null)
            {
                return false;
            }

            if (figure.State == Figure.States.Start)
            {
                game.Status = "You cannot move with this figure";
                return false;
            }

            var position = Transform(figure.Position + game.Dice.Value);

            if (figure.Position <= figure.Player.FinalPosition)
            {
                if (position > figure.Player.FinalPosition)
                {
                    var diff = figure.Player.FinalPosition - position;

                    position = Math.Abs(diff) - 1;
                }
            }

            var cell = FigureByPosition(game.Players, position);
            if (cell != null && figure.Player == cell.Player)
            {
                game.Status = "You cannot move with this figure";
                return false;
            }

            if (cell != null)
            {
                game.Status = game.CurrentPlayer.Name + " kicked " + cell.Player.Name + " figure";
                cell.Kick();
            }

            return true;
        }

        public bool MovePlayer(Game game, int figureIndex)
        {
            var figure = game.CurrentPlayer.FigureByPosition(figureIndex, Size());

            if (figure == null)
            {
                return false;
            }

            if (!PlayerCanMove(game, figure))
            {
                return false;
            }

            var position = Transform(figure.Position + game.Dice.Value);

            if (figure.Position <= figure.Player.FinalPosition)
            {
                if (position > figure.Player.FinalPosition)
                {
                    var diff = figure.Player.FinalPosition - position;

                    position = Math.Abs(diff) - 1;

                    figure.Home();
                    game.Status = figure.Player.Name + " moved figure to home {position: " + position + "|diff:" +
                                  diff + "}";

                    figure.NewPosition(position);
                    return true;
                }
            }

            figure.NewPosition(position);
            return true;
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
                        case Cell.F:

                            if (type == Cell.S)
                            {
                                cell = '*';
                            }

                            var figure = FigureByPosition(players, index);
                            if (figure != null)
                            {
                                if (figure.State == Figure.States.Playing)
                                {
                                    cell = (char) (figure.Index + 48);
                                    cell = figure.Player.Symbol;
                                }
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

                            cell = '#';

                            if (-1 < owner && owner <= players.Count)
                            {
                                cell = players[owner - 1].HasFigureAtHome(index) ? players[owner - 1].Symbol : '#';
                            }

                            builder.Append(" " + cell + " ");
                            break;
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