namespace Ludo
{
    public class Figure
    {
        public enum States
        {
            Start,
            Home,
            Playing
        }

        public Player Player { get; }
        public int Position { get; private set; }

        public int Index { get; set; }

        public States State { get; private set; }

        public Figure(Player player, int position)
        {
            Position = position;
            Player = player;

            State = States.Start;
        }

        public void PlaceAtStart()
        {
            Position = Player.StartPosition;

            State = States.Playing;
        }

        public void NewPosition(int position)
        {
            Position = position;
        }

        public void Kick()
        {
            Position = -1;
            
            Player.KickTrigger();

            State = States.Start;
        }

        public void Home()
        {
            Player.HomeTrigger();

            State = States.Home;
        }

        public void Reset()
        {
            State = States.Start;
            Position = -1;
        }
    }
}