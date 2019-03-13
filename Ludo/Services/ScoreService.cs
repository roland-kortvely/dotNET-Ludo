using System.Collections;
using System.Linq;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class ScoreService : IScoreService
    {
        public void Add(Score score)
        {
            var db = Game.Instance.DB;

            db.Add(score);
            db.SaveChanges();
        }

        public Score Get(string name)
        {
            var db = Game.Instance.DB;

            return db.Scores.SingleOrDefault(s => s.Name == name);
        }

        public IList GetTop()
        {
            var db = Game.Instance.DB;

            return (from s in db.Scores orderby s.Points descending select s)
                .Take(3)
                .ToList();
        }

        public IList GetAll()
        {
            var db = Game.Instance.DB;

            return (from s in db.Scores orderby s.Points descending select s)
                .ToList();
        }

        public void Clear()
        {
            var db = Game.Instance.DB;

            db.Database.ExecuteSqlCommand("DELETE FROM Scores");
        }

        public void Save()
        {
            var db = Game.Instance.DB;

            db.SaveChanges();
        }
    }
}