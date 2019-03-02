namespace Ludo
{
    public class DebugMode : IGameMode
    {
        public void Start(Game game)
        {
            Debug.Listen(new DebugController(game));
        }

        public void Loop(Game game)
        {
        }
    }
}