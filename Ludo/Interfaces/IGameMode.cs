using Ludo.Models;

namespace Ludo.Interfaces
{
    public interface IGameMode
    {
        void Start(Game game);
        void Loop(Game game);
        void Dispose(Game game);
    }
}