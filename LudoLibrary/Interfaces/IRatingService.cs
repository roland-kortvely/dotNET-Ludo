using LudoLibrary.Models;

namespace LudoLibrary.Interfaces
{
    public interface IRatingService : IService<Rating>
    {
        float AverageRating();
        void Rate(int stars, string content);
    }
}