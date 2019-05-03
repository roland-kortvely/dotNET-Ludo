using System;

namespace Ludo.Models
{
    public class Figure
    {
        public enum States
        {
            Start,
            Home,
            Playing
        }

        public Figure(Player player, int index)
        {
            Player = player;

            Position = -1;
            AbstractPosition = 0;

            State = States.Start;

            Index = index;
        }

        public Player Player { get; }
        public int Position { get; private set; }

        public int AbstractPosition { get; private set; }

        public int Index { get; set; }

        public States State { get; private set; }

        public void PlaceAtStart()
        {
            Position = Player.StartPosition;

            State = States.Playing;
        }

        public void NewPosition(int position, Dice dice)
        {
            Position = position;
            AbstractPosition += dice.Value;
        }

        public void Kick()
        {
            Position = -1;
            AbstractPosition = 0;
            

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
            Position = -1;
            AbstractPosition = 0;

            State = States.Start;
        }
    }
}