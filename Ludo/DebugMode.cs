namespace Ludo
{
    public class DebugMode : IGameMode
    {
        public void Start(Game game)
        {
            Debug.Listen(new DebugController(game));
            
            game.Dice.Set(6);
            
            game.Draw();
        }

        public void Loop(Game game)
        {
        }
    }
}