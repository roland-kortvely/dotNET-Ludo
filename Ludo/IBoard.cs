using System.Collections.Generic;

namespace Ludo
{
    public interface IBoard
    {
        string Render(List<Player> players);

        bool PlayerCanPlaceFigure(Dice dice, List<Player> players, Player player);
        bool PlayerCanMove(Dice dice, List<Player> players, Player player);

        void MovePlayer(Dice dice, List<Player> players, Player player);

        int MaxPlayers();
        int PlayerFigures();
        int StartPosition(int index);

        int Size();
        Board.Cell[,] Map();
        int[,] Players();
        int[,] MapIndex();
    }
}