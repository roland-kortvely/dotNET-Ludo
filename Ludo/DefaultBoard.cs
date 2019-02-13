using System.Text;

namespace Ludo
{
    public class DefaultBoard : IBoard
    {
        private readonly char[][] _map =
        {
            new[] {'S', ' ', 'S', ' ', 'F', 'F', 'F', ' ', 'S', ' ', 'S'},
            new[] {' ', ' ', ' ', ' ', 'F', 'D', 'F', ' ', ' ', ' ', ' '},
            new[] {'S', ' ', 'S', ' ', 'F', 'D', 'F', ' ', 'S', ' ', 'S'},
            new[] {' ', ' ', ' ', ' ', 'F', 'D', 'F', ' ', ' ', ' ', ' '},
            new[] {'F', 'F', 'F', 'F', 'F', 'D', 'F', 'F', 'F', 'F', 'F'},
            new[] {'F', 'D', 'D', 'D', 'D', ' ', 'D', 'D', 'D', 'D', 'F'},
            new[] {'F', 'F', 'F', 'F', 'F', 'D', 'F', 'F', 'F', 'F', 'F'},
            new[] {' ', ' ', ' ', ' ', 'F', 'D', 'F', ' ', ' ', ' ', ' '},
            new[] {'S', ' ', 'S', ' ', 'F', 'D', 'F', ' ', 'S', ' ', 'S'},
            new[] {' ', ' ', ' ', ' ', 'F', 'D', 'F', ' ', ' ', ' ', ' '},
            new[] {'S', ' ', 'S', ' ', 'F', 'F', 'F', ' ', 'S', ' ', 'S'},
        };

        private readonly int[][] _players =
        {
            new[] {1, 0, 1, 0, 0, 0, 0, 0, 4, 0, 4},
            new[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            new[] {1, 0, 1, 0, 0, 1, 0, 0, 4, 0, 4},
            new[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            new[] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            new[] {0, 2, 2, 2, 2, 0, 4, 4, 4, 4, 0},
            new[] {0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0},
            new[] {0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0},
            new[] {2, 0, 2, 0, 0, 3, 0, 0, 3, 0, 3},
            new[] {0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0},
            new[] {2, 0, 2, 0, 0, 0, 0, 0, 3, 0, 3}
        };

        private readonly int[][] _mapIndex =
        {
            new[] {0, -1, 1, -1, 0, 39, 38, -1, 1, -1, 0},
            new[] {-1, -1, -1, -1, 1, 3, 37, -1, -1, -1, -1},
            new[] {2, -1, 3, -1, 2, 2, 36, -1, 3, -1, 2},
            new[] {-1, -1, -1, -1, 3, 1, 35, -1, -1, -1, -1},
            new[] {8, 7, 6, 5, 4, 0, 34, 33, 32, 31, 30},
            new[] {9, 3, 2, 1, 0, -1, 0, 1, 2, 3, 29},
            new[] {10, 11, 12, 13, 14, 0, 24, 25, 26, 27, 28},
            new[] {-1, -1, -1, -1, 15, 1, 23, -1, -1, -1, -1},
            new[] {2, -1, 3, -1, 16, 2, 22, -1, 3, -1, 2},
            new[] {-1, -1, -1, -1, 17, 3, 21, -1, -1, -1, -1},
            new[] {0, -1, 1, -1, 18, 19, 20, -1, 1, -1, 0},
        };

        public string Render()
        {
            var builder = new StringBuilder();

            for (var i = 0; i < this._map.Length; i++)
            {
                for (var k = 0; k < this._map[i].Length; k++)
                {
                    var type = this._map[i][k];
                    var owner = this._players[i][k];
                    var index = this._mapIndex[i][k];

                    switch (type)
                    {
                        case 'F':
                            //Figure figure = this.FindFigure(index);
                            //builder.Append('[').Append(figure != null ? figure.Sign : ' ').Append(']');
                            builder.Append('[').Append(' ').Append(']');
                            break;
                        case 'S':
                            //builder.Append(players[owner - 1].PrintStartFigure(index));
                            builder.Append("(" + ' ' + ")");
                            break;
                        case 'D':
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