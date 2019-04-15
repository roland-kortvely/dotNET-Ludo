using System.Collections.Generic;
using LudoLibrary.Models;

namespace LudoLibrary.Interfaces
{
    public interface IScoreService : IService<Score>
    {
        IList<Score> GetTop();

        void IncreaseScore(string name);
    }
}