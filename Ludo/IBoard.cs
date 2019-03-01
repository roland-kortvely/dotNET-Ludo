using System.Collections.Generic;

namespace Ludo
{
    public interface IBoard
    {
        string Render(List<Player> players);

        bool PlayerCanPlaceFigure(Dice dice, List<Player> players, Player player);
        bool PlayerCanMove(Game game, Figure figure);

        void MovePlayer(Game game, Figure figure);

        int MaxPlayers();
        int PlayerFigures();
        int StartPosition(int index);

        int Size();
        Board.Cell[,] Map();
        int[,] Players();
        int[,] MapIndex();
    }
}