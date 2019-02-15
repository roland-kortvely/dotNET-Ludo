using System.Collections.Generic;

namespace Ludo
{
    public interface IBoard
    {
        string Render(List<Player> players);

        int MaxPlayers();
        int PlayerFigures();

        Board.Cell[,] Map();
        int[,] Players();
        int[,] MapIndex();
    }
}