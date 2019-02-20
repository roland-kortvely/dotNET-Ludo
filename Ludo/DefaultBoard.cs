namespace Ludo
{
    using static Board.Cell;

    public class DefaultBoard : Board
    {
        private readonly Cell[,] _map =
        {
            {P, _, P, _, R, R, S, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, R, H, R, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {S, R, R, R, R, H, R, R, R, R, R},
            {R, H, H, H, H, _, H, H, H, H, R},
            {R, R, R, R, R, H, R, R, R, R, S},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, R, H, R, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, S, R, R, _, P, _, P},
        };

        private readonly int[,] _players =
        {
            {1, 0, 1, 0, 0, 0, 2, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 2, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0},
            {0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 3},
            {0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0},
            {4, 0, 4, 0, 0, 4, 0, 0, 3, 0, 3},
            {0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0},
            {4, 0, 4, 0, 4, 0, 0, 0, 3, 0, 3}
        };

        private readonly int[,] _mapIndex =
        {
            { 0,  0,  1,  0,  8,  9, 10,  0,  1,  0,  0},
            { 0,  0,  0,  0,  7,  0, 11,  0,  0,  0,  0},
            { 2,  0,  3,  0,  6,  1, 12,  0,  3,  0,  2},
            { 0,  0,  0,  0,  5,  2, 13,  0,  0,  0,  0},
            { 0,  1,  2,  3,  4,  3, 14, 15, 16, 17, 18},
            {39,  0,  1,  2,  3,  0,  3,  2,  1,  0, 19},
            {38, 37, 36, 35, 34,  3, 24, 23, 22, 21, 20},
            { 0,  0,  0,  0, 33,  2, 25,  0,  0,  0,  0},
            { 2,  0,  3,  0, 32,  1, 26,  0,  3,  0,  2},
            { 0,  0,  0,  0, 31,  0, 27,  0,  0,  0,  0},
            { 0,  0,  1,  0, 30, 29, 28,  0,  1,  0,  0},
        };

        public override int MaxPlayers() => 4;
        public override int PlayerFigures() => 4;
        public override int Size() => 40;

        public override Cell[,] Map() => _map;
        public override int[,] Players() => _players;
        public override int[,] MapIndex() => _mapIndex;
    }
}