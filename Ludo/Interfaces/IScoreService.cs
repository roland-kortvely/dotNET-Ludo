using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IScoreService
    {
        void Add(Score score);

        IList GetTop();

        void Clear();
    }
}