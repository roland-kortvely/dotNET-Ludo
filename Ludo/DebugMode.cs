namespace Ludo
{
    public class DebugMode : IGameMode
    {
        public void Start(Game game)
        {
            Debug.Listen(new DebugController(game));
            
            game.Dice.Set(6);
            
            game.RefreshUserInterface();
        }

        public void Loop(Game game)
        {
        }

        public void Reset(Game game)
        {
            
        }
    }
}