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
        void Clear();
        
        IList GetAll();
        IList GetTop();

        void IncreaseScore(string name);
    }
}