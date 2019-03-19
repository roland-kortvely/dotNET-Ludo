using System.Collections;
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
            var db = new LudoContext();

            db.Add(score);
            db.SaveChanges();
        }

        public Score Get(int id)
        {
            var db = new LudoContext();

            return db.Scores.Find(id);
        }

        public void Delete(int id)
        {
            var db = new LudoContext();

            var score = db.Scores.Find(id);
            if (score == null)
            {
                return;
            }

            db.Scores.Remove(score);
            db.SaveChanges();
        }

        public void Update(int id, Score data)
        {
            var db = new LudoContext();

            var score = db.Scores.Find(id);
            if (score == null)
            {
                return;
            }

            score.Name = data.Name;
            score.Points = data.Points;

            db.SaveChanges();
        }

        public IList GetTop()
        {
            var db = new LudoContext();

            return (from s in db.Scores orderby s.Points descending select s)
                .Take(3)
                .ToList();
        }

        public IList GetAll()
        {
            var db = new LudoContext();

            return (from s in db.Scores orderby s.Points descending select s)
                .ToList();
        }

        public void Clear()
        {
            var db = new LudoContext();

            db.Database.ExecuteSqlCommand("DELETE FROM Scores");
        }

        public void IncreaseScore(string name)
        {
            if (name == null)
            {
                return;
            }

            var db = new LudoContext();

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