namespace Ludo
{
    public interface IBoard
    {
        string Render();

        Board.Cell[,] Map();
        int[,] Players();
        int[,] MapIndex();
    }
}