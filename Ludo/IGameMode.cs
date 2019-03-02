namespace Ludo
{
    public interface IGameMode
    {
        void Start(Game game);
        void Loop(Game game);
    }
}