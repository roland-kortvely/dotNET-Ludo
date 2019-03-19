using System;
using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface ICommentService
    {
        void Add(Comment comment);
        Comment Get(int id);
        void Delete(int id);
        void Update(int id, Comment data);
        void Clear();
        
        IList GetAll();
        void NewComment(string name, string content);
    }
}