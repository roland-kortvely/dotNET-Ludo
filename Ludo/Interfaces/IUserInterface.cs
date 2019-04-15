using Ludo.Models;

namespace Ludo.Interfaces
{
    public interface IUserInterface
    {
        void Start(Game game);
        void Loop(Game game);
        void Render(Game game);
        void Reset(Game game);
    }
}