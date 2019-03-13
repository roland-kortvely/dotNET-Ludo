using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IScoreService
    {
        void Add(Score score);

        Score Get(string name);

        IList GetAll();
        IList GetTop();

        void Clear();

        void Save();
    }
}