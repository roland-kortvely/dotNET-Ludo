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
        protected abstract int[,] Owners();
        protected abstract int[,] MapIndex();
        public abstract ConsoleColor Colors(int index);

        public enum Cell
        {
            _, //None
            R, //Road
            H, //Home
            P, //Player
            S, //Start
            F  //Final cell to home
        }

        private int Transform(int position)
        {
            return position >= Size() ? position - Size() : position;
        }

        private Cell CellTypeByPosition(int position)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    var mapIndex = MapIndex()[i, j];

                    switch (type)
                    {
                        case Cell.S:
                        case Cell.R:
                        case Cell.F:
                            
                            if (mapIndex == position)
                            {
                                return type;
                            }

                            break;
                    }
                }
            }

            return Cell._;
        }

        public int StartPosition(int index)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    var owner = Owners()[i, j];
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
                    var owner = Owners()[i, j];
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

        public bool PlayerCanPlaceFigure(Game game, Player player)
        {
            return game.Dice.Value == 6 && player.HasFigureAtStart() && CanPlaceFigureAtStart(game.Players, player);
        }

        public bool PlayerCanMove(Game game, Figure figure)
        {
            if (figure == null)
            {
                return false;
            }

            if (figure.State == Figure.States.Start)
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

                    if (figure.Player.HasFigureAtHome(position) || position > MaxPlayers() - 1)
                    {
                        return false;
                    }
                }
            }

            if (figure.State == Figure.States.Home)
            {
                if (figure.Player.HasFigureAtHome(position) || position > MaxPlayers() - 1)
                {
                    return false;
                }
            }

            var cell = FigureByPosition(game.Players, position);
            if (cell != null && figure.Player == cell.Player)
            {
                return false;
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

                    figure.Home();
                    game.Status = figure.Player.Name + " moved figure to home";

                    figure.NewPosition(position);
                    return true;
                }
            }

            var cell = FigureByPosition(game.Players, position);
            if (cell != null && figure.State == Figure.States.Playing)
            {
                if (CellTypeByPosition(position) != Cell.S)
                {
                    game.Status = game.CurrentPlayer.Name + " kicked " + cell.Player.Name + " figure";
                    cell.Kick();
                }
            }

            figure.NewPosition(position);
            return true;
        }

        public string Render(Game game)
        {
            if (game.Players == null)
            {
                return "";
            }

            var builder = new StringBuilder();

            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    var owner = Owners()[i, j];
                    var index = MapIndex()[i, j];

                    var cell = ' ';

                    switch (type)
                    {
                        case Cell.S:
                        case Cell.R:
                        case Cell.F:

                            Console.Write("[");

                            if (type == Cell.S)
                            {
                                Console.ForegroundColor = Colors(owner - 1);

                                cell = '*';
                            }

                            var figure = FigureByPosition(game.Players, index);
                            if (figure != null)
                            {
                                if (figure.State == Figure.States.Playing)
                                {
                                    //cell = (char) (figure.Index + 48);
                                    cell = figure.Player.Symbol;

                                    Console.ForegroundColor = Colors(figure.Player.Index);
                                }
                            }

                            builder.Append("[" + cell + "]");
                            Console.Write(cell);

                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("]");
                            break;
                        case Cell.P:
                            Console.Write("(");

                            Console.ForegroundColor = Colors(owner - 1);

                            if (-1 < owner && owner <= game.Players.Count)
                            {
                                cell = game.Players[owner - 1].HasFigureAtStart(index)
                                    ? game.Players[owner - 1].Symbol
                                    : ' ';
                            }

                            builder.Append("(" + cell + ")");
                            Console.Write(cell);

                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.Write(")");
                            break;
                        case Cell.H:

                            Console.ForegroundColor = Colors(owner - 1);

                            cell = '#';

                            if (-1 < owner && owner <= game.Players.Count)
                            {
                                cell = game.Players[owner - 1].HasFigureAtHome(index)
                                    ? game.Players[owner - 1].Symbol
                                    : '#';
                            }

                            builder.Append(" " + cell + " ");
                            Console.Write(" " + cell + " ");

                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        default:
                            builder.Append("   ");
                            Console.Write("   ");

                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                    }
                }

                Console.WriteLine();
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}