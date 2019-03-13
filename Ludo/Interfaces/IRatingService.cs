using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IRatingService
    {
        void Add(Rating rating);

        IList GetAll();

        float AverageRating();

        void Rate(int stars, string content);

        void Clear();
    }
}