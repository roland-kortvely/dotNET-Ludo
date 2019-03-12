using System;
using System.Collections.Generic;
using System.Linq;
using Ludo.Interfaces;

namespace Ludo.Entities
{
    public abstract class Board : IBoard
    {
        public enum Cell
        {
            _, //None
            R, //Road
            H, //Home
            P, //Player
            S, //Start
            F //Final cell to home
        }

        public abstract int MaxPlayers();
        public abstract int PlayerFigures();

        public abstract int Size();
        public abstract Cell[,] Map();
        public abstract int[,] Owners();
        public abstract int[,] MapIndex();
        public abstract ConsoleColor Colors(int index);

        public int StartPosition(int index)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            for (var j = 0; j <= Map().GetUpperBound(1); j++)
            {
                var type = Map()[i, j];
                var owner = Owners()[i, j];
                var mapIndex = MapIndex()[i, j];

                switch (type)
                {
                    case Cell.S:
                        if (owner == index) return mapIndex;

                        break;
                }
            }

            return -1;
        }

        public int FinalPosition(int index)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            for (var j = 0; j <= Map().GetUpperBound(1); j++)
            {
                var type = Map()[i, j];
                var owner = Owners()[i, j];
                var mapIndex = MapIndex()[i, j];

                switch (type)
                {
                    case Cell.F:
                        if (owner == index) return mapIndex;

                        break;
                }
            }

            return -1;
        }

        public Figure FigureByPosition(Game game, int position)
        {
            Figure figure = null;

            foreach (var player in game.Players)
            foreach (var current in player.Figures)
            {
                if (current.Position != position) continue;

                if (current.State != Figure.States.Playing) continue;

                figure = current;

                break;
            }

            return figure;
        }

        public bool PlayerCanStartWithFigure(Game game)
        {
            if (game.Dice.Value != 6) return false;

            if (!game.CurrentPlayer.HasFigureAtStart()) return false;

            var figure = FigureByPosition(game, game.CurrentPlayer.StartPosition);
            return figure == null || figure.Player != game.CurrentPlayer;
        }

        public bool PlayerCanMove(Game game, Figure figure)
        {
            if (figure == null) return false;

            if (figure.State == Figure.States.Start) return false;

            var position = Transform(figure.Position + game.Dice.Value);

            if (figure.AbstractPosition + game.Dice.Value >= Size())
            {
                position = figure.AbstractPosition + game.Dice.Value - Size();

                if (figure.Player.HasFigureAtHome(position) || position > MaxPlayers() - 1) return false;

                if (figure.State == Figure.States.Home)
                    if (figure.Player.HasFigureAtHome(position))
                        return false;

                return true;
            }

            var cell = FigureByPosition(game, position);

            return cell == null || figure.Player != cell.Player;
        }

        public bool MovePlayer(Game game, Figure figure)
        {
            if (figure == null)
            {
                return false;
            }

            if (!PlayerCanMove(game, figure))
            {
                game.Status = "You can't move with this figure";
                return false;
            }

            var position = Transform(figure.Position + game.Dice.Value);

            if (figure.AbstractPosition + game.Dice.Value >= Size() && figure.State != Figure.States.Home)
            {
                position = figure.AbstractPosition + game.Dice.Value - Size();

                figure.Home();
                game.Status = figure.Player.Name + " moved a figure to home";

                figure.NewPosition(position, game.Dice);
                return true;
            }

            var cell = FigureByPosition(game, position);
            if (cell != null && figure.State == Figure.States.Playing)
                if (CellTypeByPosition(position) != Cell.S)
                    cell.Kick();

            figure.NewPosition(position, game.Dice);
            return true;
        }

        public bool MovePlayer(Game game, int figureIndex)
        {
            var figure = FigureByNumericIndex(game, figureIndex);

            if (figure == null) return false;

            return MovePlayer(game, figure);
        }

        private int Transform(int position)
        {
            return position >= Size() ? position - Size() : position;
        }

        private Cell CellTypeByPosition(int position)
        {
            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            for (var j = 0; j <= Map().GetUpperBound(1); j++)
            {
                var type = Map()[i, j];
                var mapIndex = MapIndex()[i, j];

                switch (type)
                {
                    case Cell.S:
                    case Cell.R:
                    case Cell.F:

                        if (mapIndex == position) return type;

                        break;
                }
            }

            return Cell._;
        }

        private Figure FigureByNumericIndex(Game game, int index)
        {
            var x = new List<Figure>();
            foreach (var figure in game.CurrentPlayer.Figures)
                if (figure.State != Figure.States.Start)
                    x.Add(figure);

            x = x.OrderBy(figure1 =>
            {
                var fig1 = figure1.Position;
                if (figure1.State == Figure.States.Home) return fig1 + Size();

                return fig1 >= figure1.Player.StartPosition
                    ? fig1 - figure1.Player.StartPosition
                    : fig1 + (Size() - figure1.Player.StartPosition);
            }).ToList();


            //DEBUG indexing on board
            for (var i = 0; i < x.Count; i++) x[i].Index = i + 1;

            return x.Count >= index ? x[index - 1] : null;
        }
    }
}