namespace Ludo
{
    internal static class Ludo
    {
        private static void Main()
        {
            var game = new Game(new DefaultBoard());

            game.NewPlayer("Bajka", 'B');
            game.NewPlayer("Rolko", 'R');

            game.Dice.Roll();

            game.Draw();
        }
    }
}