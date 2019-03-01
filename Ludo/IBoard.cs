using System.Collections.Generic;

namespace Ludo
{
    public interface IBoard
    {
        string Render(List<Player> players);

        bool PlayerCanPlaceFigure(Dice dice, List<Player> players, Player player);
        bool PlayerCanMove(Game game, Figure figure);

        bool MovePlayer(Game game, int figureIndex);

        int MaxPlayers();
        int PlayerFigures();
        int StartPosition(int index);
        int FinalPosition(int index);
    }
}