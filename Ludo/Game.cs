using System;
using System.Text;

namespace Ludo
{
    public class Game
    {
        private readonly IBoard board;

        public Game(IBoard board)
        {
            this.board = board;
        }

        public void Draw()
        {
            var builder = new StringBuilder();

            builder.Append("Ludo..").AppendLine();

            builder.Append("Dice:..").AppendLine();

            builder.Append(this.board.Render()).AppendLine();

            Console.WriteLine(builder.ToString());
        }
    }
}