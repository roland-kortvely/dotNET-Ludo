using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class CommentService : ICommentService
    {
        private readonly LudoContext _db;

        public CommentService() : this(new LudoContext())
        {
        }

        public CommentService(LudoContext context)
        {
            _db = context;
        }

        public void Add(Comment comment)
        {
            _db.Add(comment);
            _db.SaveChanges();
        }

        public Comment Get(int id)
        {
            return _db.Comments.Find(id);
        }

        public void Delete(int id)
        {
            var entity = _db.Comments.Find(id);
            if (entity == null) return;

            _db.Comments.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(int id, Comment data)
        {
            var entity = _db.Comments.Find(id);
            if (entity == null) return;

            entity.Name = data.Name;
            entity.Content = data.Content;

            _db.SaveChanges();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Comments");
        }

        public void NewComment(string name, string content)
        {
            if (name == null) return;

            if (content == null) return;

            Add(new Comment
            {
                Name = name,
                Content = content
            });
        }

        public IList<Comment> GetAll()
        {
            return (from s in _db.Comments orderby s.Id descending select s)
                .ToList();
        }
    }
}