using System;
using Ludo.Models;

namespace Ludo.Interfaces
{
    public interface IBoard
    {
        bool PlayerCanStartWithFigure(Game game);
        bool PlayerCanStartWithFigure(Game game, Figure figure);
        bool PlayerCanMove(Game game, Figure figure);

        bool FigureKicked(Game game, Figure figure);
        
        bool MovePlayer(Game game, int figureIndex);
        bool MovePlayer(Game game, Figure figure);

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