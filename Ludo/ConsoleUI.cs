using System;
using System.Text;

namespace Ludo
{
    public class ConsoleUI : IUserInterface
    {
        public void Start(Game game)
        {
            var players = 0;

            do
            {
                Console.Write("Player mode (max " + game.Board.MaxPlayers() + "): ");

                try
                {
                    players = Convert.ToInt32(Console.ReadLine());

                    if (players < 2 || players > game.Board.MaxPlayers())
                    {
                        Console.WriteLine("Out of range..");
                    }
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
            if (game.Players.Count == 0)
            {
                return;
            }

            Console.Clear();

            var builder = new StringBuilder();

            builder.Append("Ludo by Roland Körtvely ").AppendLine(game.Mode);

            builder.AppendLine("----------------------------");

            builder.AppendLine("Use [Space] to roll the dice, [numpad] to move with a figure.");

            builder.Append("Dice: ")
                .Append(game.Dice.Value.ToString())
                .Append(" <-> Current player: ")
                .AppendLine(game.CurrentPlayer.Name);

            builder.AppendLine("Status: " + game.Status);

            Console.WriteLine(builder.ToString());

            //builder.AppendLine(Board.Render(this));
            game.Board.Render(game);
        }

        public void Reset(Game game)
        {
            //Console.CursorVisible = true;
            Console.ReadKey(true);
        }
    }
}