using LudoLibrary.Models;

namespace LudoLibrary.Interfaces
{
    public interface ICommentService : IService<Comment>
    {
        void NewComment(string name, string content);
    }
}