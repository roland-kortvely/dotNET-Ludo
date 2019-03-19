using System.Collections;
using System.Linq;
using Ludo.Database;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class CommentService : ICommentService
    {
        public void Add(Comment comment)
        {
            var db = new LudoContext();

            db.Add(comment);
            db.SaveChanges();
        }

        public Comment Get(int id)
        {
            var db = new LudoContext();

            return db.Comments.Find(id);
        }

        public void Delete(int id)
        {
            var db = new LudoContext();

            var entity = db.Comments.Find(id);
            if (entity == null)
            {
                return;
            }

            db.Comments.Remove(entity);
            db.SaveChanges();
        }

        public void Update(int id, Comment data)
        {
            var db = new LudoContext();

            var entity = db.Comments.Find(id);
            if (entity == null)
            {
                return;
            }

            entity.Name = data.Name;
            entity.Content = data.Content;

            db.SaveChanges();
        }

        public void Clear()
        {
            var db = new LudoContext();

            db.Database.ExecuteSqlCommand("DELETE FROM Comments");
        }

        public void NewComment(string name, string content)
        {
            if (name == null)
            {
                return;
            }

            if (content == null)
            {
                return;
            }

            Add(new Comment
            {
                Name = name,
                Content = content
            });
        }

        public IList GetAll()
        {
            var db = new LudoContext();

            return (from s in db.Comments orderby s.Id descending select s)
                .ToList();
        }
    }
}