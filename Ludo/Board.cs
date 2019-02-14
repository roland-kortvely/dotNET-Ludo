using System.Text;

namespace Ludo
{
    public abstract class Board : IBoard
    {
        public abstract Cell[,] Map();
        public abstract int[,] Players();
        public abstract int[,] MapIndex();

        public enum Cell
        {
            X,    //None
            P,    //Path
            H,    //Home
            S,    //Start                   
        }
        
        public string Render()
        {
            var builder = new StringBuilder();

            for (var i = 0; i <= Map().GetUpperBound(0); i++)
            {
                for (var j = 0; j <= Map().GetUpperBound(1); j++)
                {
                    var type = Map()[i, j];
                    //var owner = Players()[i, j];
                    //var index = MapIndex()[i, j];

                    switch (type)
                    {
                        case Cell.P:
                            //Figure figure = this.FindFigure(index);
                            //builder.Append('[').Append(figure != null ? figure.Sign : ' ').Append(']');
                            builder.Append('[').Append(' ').Append(']');
                            break;
                        case Cell.S:
                            //builder.Append(players[owner - 1].PrintStartFigure(index));
                            builder.Append("(" + ' ' + ")");
                            break;
                        case Cell.H:
                            //builder.Append(players[owner - 1].PrintFinishFigure(index));
                            builder.Append("   ");
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