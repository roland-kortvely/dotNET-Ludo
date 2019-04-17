using System;
using Ludo.Models;
using static Ludo.Models.Board.Cell;

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
            {P, _, P, _, _, R, F, R, _, _, P, _, P},
            {_, _, _, _, _, R, H, S, _, _, _, _, _},
            {P, _, P, _, _, R, H, R, _, _, P, _, P},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {R, S, R, R, R, R, _, R, R, R, R, R, R},
            {F, H, H, H, H, _, _, _, H, H, H, H, F},
            {R, R, R, R, R, R, _, R, R, R, R, S, R},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {_, _, _, _, _, R, H, R, _, _, _, _, _},
            {P, _, P, _, _, R, H, R, _, _, P, _, P},
            {_, _, _, _, _, S, H, R, _, _, _, _, _},
            {P, _, P, _, _, R, F, R, _, _, P, _, P}
        };

        private readonly int[,] _mapIndex =
        {
            {00, 00, 01, 00, 00, 21, 22, 23, 00, 00, 01, 00, 00},
            {00, 00, 00, 00, 00, 20, 00, 24, 00, 00, 00, 00, 00},
            {02, 00, 03, 00, 00, 19, 01, 25, 00, 00, 03, 00, 02},
            {00, 00, 00, 00, 00, 18, 02, 26, 00, 00, 00, 00, 00},
            {00, 00, 00, 00, 00, 17, 03, 27, 00, 00, 00, 00, 00},
            {11, 12, 13, 14, 15, 16, 00, 28, 29, 30, 31, 32, 33},
            {10, 00, 01, 02, 03, 00, 00, 00, 03, 02, 01, 00, 34},
            {09, 08, 07, 06, 05, 04, 00, 40, 39, 38, 37, 36, 35},
            {00, 00, 00, 00, 00, 03, 03, 41, 00, 00, 00, 00, 00},
            {00, 00, 00, 00, 00, 02, 02, 42, 00, 00, 00, 00, 00},
            {02, 00, 03, 00, 00, 01, 01, 43, 00, 00, 03, 00, 02},
            {00, 00, 00, 00, 00, 00, 00, 44, 00, 00, 00, 00, 00},
            {00, 00, 01, 00, 00, 47, 46, 45, 00, 00, 01, 00, 00}
        };

        private readonly int[,] _owners =
        {
            {3, 0, 3, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0},
            {3, 0, 3, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2},
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 3, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
            {3, 3, 3, 3, 3, 0, 0, 0, 4, 4, 4, 4, 4},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 4},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 0, 4},
            {0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 0, 4}
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