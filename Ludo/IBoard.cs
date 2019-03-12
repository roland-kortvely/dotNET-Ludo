using System;

namespace Ludo
{
    public interface IBoard
    {
        bool PlayerCanStartWithFigure(Game game, Player player);
        bool PlayerCanMove(Game game, Figure figure);

        bool MovePlayer(Game game, int figureIndex);

        int MaxPlayers();
        int PlayerFigures();
        int StartPosition(int index);
        int FinalPosition(int index);
        ConsoleColor Colors(int index);

        Figure FigureByPosition(Game game, int position);

        Board.Cell[,] Map();
        int[,] Owners();
        int[,] MapIndex();
        int Size();
    }
}