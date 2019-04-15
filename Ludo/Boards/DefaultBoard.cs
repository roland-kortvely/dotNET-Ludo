using System;
using Ludo.Models;
using static Ludo.Models.Board.Cell;

namespace Ludo.Boards
{
    public class DefaultBoard : Board
    {
        private readonly ConsoleColor[] _consoleColors =
        {
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Red,
            ConsoleColor.Green
        };

        private readonly Cell[,] _map =
        {
            {P, _, P, _, R, F, S, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, R, H, R, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {S, R, R, R, R, H, R, R, R, R, R},
            {F, H, H, H, H, _, H, H, H, H, F},
            {R, R, R, R, R, H, R, R, R, R, S},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, R, H, R, _, P, _, P},
            {_, _, _, _, R, H, R, _, _, _, _},
            {P, _, P, _, S, F, R, _, P, _, P}
        };

        private readonly int[,] _mapIndex =
        {
            {00, 00, 01, 00, 08, 09, 10, 00, 01, 00, 00},
            {00, 00, 00, 00, 07, 00, 11, 00, 00, 00, 00},
            {02, 00, 03, 00, 06, 01, 12, 00, 03, 00, 02},
            {00, 00, 00, 00, 05, 02, 13, 00, 00, 00, 00},
            {00, 01, 02, 03, 04, 03, 14, 15, 16, 17, 18},
            {39, 00, 01, 02, 03, 00, 03, 02, 01, 00, 19},
            {38, 37, 36, 35, 34, 03, 24, 23, 22, 21, 20},
            {00, 00, 00, 00, 33, 02, 25, 00, 00, 00, 00},
            {02, 00, 03, 00, 32, 01, 26, 00, 03, 00, 02},
            {00, 00, 00, 00, 31, 00, 27, 00, 00, 00, 00},
            {00, 00, 01, 00, 30, 29, 28, 00, 01, 00, 00}
        };

        private readonly int[,] _owners =
        {
            {3, 0, 3, 0, 0, 2, 2, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {3, 0, 3, 0, 0, 2, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {3, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0},
            {3, 3, 3, 3, 3, 0, 4, 4, 4, 4, 4},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 4},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 1, 0, 0, 4, 0, 4},
            {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 1, 1, 0, 0, 4, 0, 4}
        };

        public override int MaxPlayers()
        {
            return 4;
        }

        public override int PlayerFigures()
        {
            return 4;
        }

        public override ConsoleColor Colors(int index)
        {
            return _consoleColors[index];
        }

        public override int Size()
        {
            return 40;
        }

        public override Cell[,] Map()
        {
            return _map;
        }

        public override int[,] Owners()
        {
            return _owners;
        }

        public override int[,] MapIndex()
        {
            return _mapIndex;
        }
    }
}