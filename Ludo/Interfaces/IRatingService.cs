using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IRatingService
    {
        void Add(Rating rating);
        void Clear();
    }
}