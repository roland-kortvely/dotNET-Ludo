namespace Ludo
{
    using static Board.Cell;

    public class DefaultBoard : Board
    {
        private readonly Cell[,] _map =
        {
            {S, X, S, X, P, P, P, X, S, X, S},
            {X, X, X, X, P, H, P, X, X, X, X},
            {S, X, S, X, P, H, P, X, S, X, S},
            {X, X, X, X, P, H, P, X, X, X, X},
            {P, P, P, P, P, H, P, P, P, P, P},
            {P, H, H, H, H, X, H, H, H, H, P},
            {P, P, P, P, P, H, P, P, P, P, P},
            {X, X, X, X, P, H, P, X, X, X, X},
            {S, X, S, X, P, H, P, X, S, X, S},
            {X, X, X, X, P, H, P, X, X, X, X},
            {S, X, S, X, P, P, P, X, S, X, S},
        };

        private readonly int[,] _players =
        {
            {1, 0, 1, 0, 0, 0, 0, 0, 4, 0, 4},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 1, 0, 0, 4, 0, 4},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {0, 3, 3, 3, 3, 0, 4, 4, 4, 4, 0},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {3, 0, 3, 0, 0, 2, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {3, 0, 3, 0, 0, 0, 0, 0, 2, 0, 2}
        };

        private readonly int[,] _mapIndex =
        {
            {0, -1, 1, -1, 0, 39, 38, -1, 1, -1, 0},
            {-1, -1, -1, -1, 1, 3, 37, -1, -1, -1, -1},
            {2, -1, 3, -1, 2, 2, 36, -1, 3, -1, 2},
            {-1, -1, -1, -1, 3, 1, 35, -1, -1, -1, -1},
            {8, 7, 6, 5, 4, 0, 34, 33, 32, 31, 30},
            {9, 3, 2, 1, 0, -1, 0, 1, 2, 3, 29},
            {10, 11, 12, 13, 14, 0, 24, 25, 26, 27, 28},
            {-1, -1, -1, -1, 15, 1, 23, -1, -1, -1, -1},
            {2, -1, 3, -1, 16, 2, 22, -1, 3, -1, 2},
            {-1, -1, -1, -1, 17, 3, 21, -1, -1, -1, -1},
            {0, -1, 1, -1, 18, 19, 20, -1, 1, -1, 0},
        };

        public override int MaxPlayers() => 4;
        public override int PlayerFigures() => 4;

        public override Cell[,] Map() => _map;

        public override int[,] Players() => _players;

        public override int[,] MapIndex() => _mapIndex;
    }
}