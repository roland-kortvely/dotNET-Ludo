namespace Ludo
{
    public class Figure
    {
        public Player Player { get; }
        public int Position { get; private set; }

        public Figure(Player player, int position)
        {
            Position = position;
            Player = player;
        }

        public void PlaceAtStart()
        {
            NewPosition(Player.StartPosition);
        }

        public void NewPosition(int position)
        {
            Position = position;

            //Todo:: Handle hit
        }
    }
}