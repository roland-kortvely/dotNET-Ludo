using System;
using System.Text;
using Ludo.Interfaces;
using Ludo.Models;

namespace Ludo.UserInterfaces
{
    public class ConsoleUI : IUserInterface
    {
        public void Start(Game game)
        {
            Console.CursorVisible = true;

            var players = 0;

            do
            {
                Console.Write("Player mode (max " + game.Board.MaxPlayers() + "): ");

                try
                {
                    players = Convert.ToInt32(Console.ReadLine());

                    if (players < 2 || players > game.Board.MaxPlayers()) Console.WriteLine("Out of range..");
                }
                catch (Exception e)
                {
                    game.Status = e.ToString();
                }
            } while (players < 2 || players > game.Board.MaxPlayers());


            for (var i = 0; i < players; i++)
            {
                Console.Write("Player " + (i + 1) + " name: ");
                var name = Console.ReadLine();

                Console.Write("Player " + (i + 1) + " symbol: ");
                var symbol = Console.Read();
                Console.ReadLine();

                game.NewPlayer(name, (char) symbol);
            }

            Console.CursorVisible = false;
        }

        public void Loop(Game game)
        {
        }

        public void Render(Game game)
        {
            if (game.Players.Count == 0) return;

            Console.Clear();

            var builder = new StringBuilder();

            builder.Append("Ludo by Roland Körtvely ").AppendLine(game.Mode);

            builder.AppendLine("----------------------------");

            builder.AppendLine(
                "Use [Space] to roll the dice, [S] to start with a figure and [numpad] to move with them.");

            builder.Append("Dice: ")
                .Append(game.Dice.Value.ToString())
                .Append(" <-> Current player: ")
                .AppendLine(game.CurrentPlayer.Name);

            builder.AppendLine("Status: " + game.Status);

            Console.WriteLine(builder.ToString());

            //builder.AppendLine(Board.Render(this));
            RenderBoard(game);
        }

        public void Reset(Game game)
        {
        }

        private void RenderBoard(Game game)
        {
            if (game?.Board == null) return;

            for (var i = 0; i <= game.Board.Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= game.Board.Map().GetUpperBound(1); j++)
                {
                    var type = game.Board.Map()[i, j];
                    var owner = game.Board.Owners()[i, j];
                    var index = game.Board.MapIndex()[i, j];

                    var cell = ' ';

                    switch (type)
                    {
                        case Board.Cell.S:
                        case Board.Cell.R:
                        case Board.Cell.F:

                            Console.Write("[");

                            if (type == Board.Cell.S)
                            {
                                Console.ForegroundColor = game.Board.Colors(owner - 1);

                                cell = '*';
                            }

                            var figure = game.Board.FigureByPosition(game, index);
                            if (figure != null)
                                if (figure.State == Figure.States.Playing)
                                {
                                    //cell = (char) (figure.Index + 48);
                                    cell = figure.Player.Symbol;

                                    Console.ForegroundColor = game.Board.Colors(figure.Player.Index);
                                }

                            Console.Write(cell);

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("]");
                            break;
                        case Board.Cell.P:
                            Console.Write("(");

                            Console.ForegroundColor = game.Board.Colors(owner - 1);

                            if (-1 < owner && owner <= game.Players.Count)
                                cell = game.Players[owner - 1].HasFigureAtStart(index)
                                    ? game.Players[owner - 1].Symbol
                                    : ' ';

                            Console.Write(cell);

                            Console.ForegroundColor = ConsoleColor.White;

                            Console.Write(")");
                            break;
                        case Board.Cell.H:

                            Console.ForegroundColor = game.Board.Colors(owner - 1);

                            cell = '#';

                            if (-1 < owner && owner <= game.Players.Count)
                                cell = game.Players[owner - 1].HasFigureAtHome(index)
                                    ? game.Players[owner - 1].Symbol
                                    : '#';

                            Console.Write(" " + cell + " ");

                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default:
                            Console.Write("   ");

                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}