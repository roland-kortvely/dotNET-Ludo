using System.Collections.Generic;
using System.Linq;
using Ludo.Database;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class ScoreService : IScoreService
    {
        public void Add(Score score)
        {
            using (var db = new LudoContext())
            {
                db.Add(score);
                db.SaveChanges();
            }
        }

        public Score Get(int id)
        {
            using (var db = new LudoContext())
            {
                return db.Scores.Find(id);
            }
        }

        public void Delete(int id)
        {
            using (var db = new LudoContext())
            {
                var entity = db.Scores.Find(id);
                if (entity == null)
                {
                    return;
                }

                db.Scores.Remove(entity);
                db.SaveChanges();
            }
        }

        public void Update(int id, Score data)
        {
            using (var db = new LudoContext())
            {
                var entity = db.Scores.Find(id);
                if (entity == null)
                {
                    return;
                }

                entity.Name = data.Name;
                entity.Points = data.Points;

                db.SaveChanges();
            }
        }

        public IList<Score> GetTop()
        {
            using (var db = new LudoContext())
            {
                return (from s in db.Scores orderby s.Points descending select s)
                    .Take(3)
                    .ToList();
            }
        }

        public IList<Score> GetAll()
        {
            using (var db = new LudoContext())
            {
                return (from s in db.Scores orderby s.Points descending select s)
                    .ToList();
            }
        }

        public void Clear()
        {
            using (var db = new LudoContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Scores");
            }
        }

        public void IncreaseScore(string name)
        {
            if (name == null)
            {
                return;
            }

            using (var db = new LudoContext())
            {
                var score = db.Scores.SingleOrDefault(s => s.Name == name);
                ;
                if (score != null)
                {
                    score.Points += 10;
                }
                else
                {
                    db.Add(new Score {Name = name, Points = 10});
                }

                db.SaveChanges();
            }
        }
    }
}