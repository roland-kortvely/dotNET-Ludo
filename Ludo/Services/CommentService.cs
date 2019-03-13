using System.Collections;
using System.Linq;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class CommentService : ICommentService
    {
        public void Add(Comment comment)
        {
            var db = Game.Instance.DB;

            db.Add(comment);
            db.SaveChanges();
        }

        public void Clear()
        {
            var db = Game.Instance.DB;

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
            var db = Game.Instance.DB;

            return (from s in db.Comments orderby s.Id descending select s)
                .ToList();
        }
    }
}