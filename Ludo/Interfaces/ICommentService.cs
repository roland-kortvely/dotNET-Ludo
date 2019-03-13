using System;
using System.Collections;
using Ludo.Entities;

namespace Ludo.Interfaces
{
    public interface ICommentService
    {
        void Add(Comment comment);
        IList GetAll();
        void Clear();
        void NewComment(string name, string content);
    }
}