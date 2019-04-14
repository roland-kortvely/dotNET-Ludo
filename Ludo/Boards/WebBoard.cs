using System;
using Ludo.Entities;
using static Ludo.Entities.Board.Cell;

namespace Ludo.Boards
{
    public class WebBoard : Board
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
            {P, _, P, _, _, R, F, S, _, _, P, _, P},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {P, _, P, _, _, R, H, R, _, _, P, _, P},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {S, R, R, R, R, R, _, R, R, R, R, R, R},
            {F, H, H, H, H, _, _, _, H, H, H, H, F},
            {R, R, R, R, R, R, _, R, R, R, R, R, S},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {P, _, P, _, _, R, H, R, _, _, P, _, P},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {P, _, P, _, _, S, F, R, _, _, P, _, P}
        };

        private readonly int[,] _mapIndex =
        {
            {00, 00, 01, 00, 00, 10, 11, 12, 00, 00, 01, 00, 00},
            {00, 00, 00, 00, 00, 09, 00, 13, 00, 00, 00, 00, 00},
            {02, 00, 03, 00, 00, 08, 01, 14, 00, 00, 03, 00, 02},
            {00, 00, 00, 00, 00, 07, 02, 15, 00, 00, 00, 00, 00},
            {00, 00, 00, 00, 00, 06, 03, 16, 00, 00, 00, 00, 00},
            {00, 01, 02, 03, 04, 05, 00, 17, 18, 19, 20, 21, 22},
            {47, 00, 01, 02, 03, 00, 00, 00, 03, 02, 01, 00, 23},
            {46, 45, 44, 43, 42, 41, 00, 29, 28, 27, 26, 25, 24},
            {00, 00, 00, 00, 00, 40, 03, 30, 00, 00, 00, 00, 00},
            {00, 00, 00, 00, 00, 39, 02, 31, 00, 00, 00, 00, 00},
            {02, 00, 03, 00, 00, 38, 01, 32, 00, 00, 03, 00, 02},
            {00, 00, 00, 00, 00, 37, 00, 33, 00, 00, 00, 00, 00},
            {00, 00, 01, 00, 00, 36, 35, 34, 00, 00, 01, 00, 00}
        };

        private readonly int[,] _owners =
        {
            {3, 0, 3, 0, 0, 0, 2, 2, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {3, 0, 3, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {3, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {3, 3, 3, 3, 3, 0, 0, 0, 4, 4, 4, 4, 4},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 4},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 0, 4},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 1, 1, 0, 0, 0, 4, 0, 4}
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
            return 48;
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