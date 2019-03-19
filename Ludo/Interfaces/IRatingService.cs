using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IRatingService
    {
        void Add(Rating rating);
        Rating Get(int id);
        void Delete(int id);
        void Update(int id, Rating data);
        void Clear();

        IList GetAll();
        float AverageRating();
        void Rate(int stars, string content);
    }
}