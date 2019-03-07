using System.Collections;

namespace Ludo
{
    public interface IScoreService
    {
        void Add(Score score);

        IList GetTop();

        void Clear();
    }
}