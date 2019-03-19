using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface IScoreService
    {
        void Add(Score score);
        Score Get(int id);
        void Delete(int id);
        void Update(int id, Score data);
        IList GetAll();
        IList GetTop();

        void Clear();
        void IncreaseScore(string name);
    }
}