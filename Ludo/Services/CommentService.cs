using System;
using Ludo.Entities;
using Ludo.Interfaces;

namespace Ludo.Services
{
    public class CommentService : ICommentService
    {
        private Game _game;

        public CommentService(Game game)
        {
            _game = game;
        }

        public void Add(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}