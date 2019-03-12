using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface ICommentService
    {
        void Add(Comment comment);
        void Clear();
    }
}