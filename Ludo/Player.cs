using System.Collections.Generic;

namespace Ludo
{
    public class Player
    {
        private int FiguresHome { get; set; }
        private int FiguresStart { get; set; }

        public string Name { get; }
        public char Symbol { get; }
        public List<Figure> Figures { get; }

        private bool FirstMove { get; set; }
        public bool ExtraMove { get; private set; }

        public int Index { get; }

        public int StartPosition { get; }
        public int FinalPosition { get; }

        public Player(int index, string name, char symbol, int figuresStart, int startPosition,
            int finalPosition)
        {
            Index = index;
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

            FirstMove = true;
            ExtraMove = false;
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

        public bool PlaceFigure()
        {
            if (FiguresStart <= 0)
            {
                return false;
            }

            foreach (var figure in Figures)
            {
                if (figure.State != Figure.States.Start)
                {
                    continue;
                }

                figure.PlaceAtStart();
                FiguresStart--;
                return true;
            }

            return false;
        }

        public void KickTrigger()
        {
            FiguresStart++;
        }

        public void HomeTrigger()
        {
            FiguresHome++;
        }

        public bool MovePossible(Game game)
        {
            foreach (var figure in Figures)
            {
                switch (figure.State)
                {
                    case Figure.States.Start:
                        if (game.Board.PlayerCanPlaceFigure(game, this))
                        {
                            return true;
                        }

                        break;
                    case Figure.States.Home:
                    case Figure.States.Playing:

                        if (game.Board.PlayerCanMove(game, figure))
                        {
                            return true;
                        }

                        break;
                    default:
                        continue;
                }
            }

            return false;
        }

        public void Turn(Game game)
        {
            ExtraMove = false;

            if (!FirstMove)
            {
                game.Status = "Roll the dice";
                game.Draw();
                InputController.Roll(game);

                if (game.Dice.Value == 6)
                {
                    ExtraMove = true;
                }
            }
            else
            {
                FirstMove = false;

                game.Dice.Set(6);
                game.Status = "Place your first figure";
                game.Draw();
                InputController.PlaceFigure(game);

                game.Status = "Roll the dice";
                game.Draw();
                InputController.Roll(game);
            }

            game.Status = "Move with figure";
            game.Draw();
            InputController.MovePlayer(game);
        }
    }
}