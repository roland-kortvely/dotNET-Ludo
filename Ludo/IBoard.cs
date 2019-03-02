using System;

namespace Ludo
{
    public interface IBoard
    {
        string Render(Game game);

        bool PlayerCanPlaceFigure(Game game, Player player);
        bool PlayerCanMove(Game game, Figure figure);

        bool MovePlayer(Game game, int figureIndex);

        int MaxPlayers();
        int PlayerFigures();
        int StartPosition(int index);
        int FinalPosition(int index);
        ConsoleColor Colors(int index);
    }
}