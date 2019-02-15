namespace Ludo
{
    public class Figure
    {
        public Player Player { get; }
        public int Position { get; }

        public Figure(Player player, int position)
        {
            Position = position;
            Player = player;
        }
    }
}