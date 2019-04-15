using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class ScoreService : IScoreService
    {
        private readonly LudoContext _db;

        public ScoreService() : this(new LudoContext())
        {
        }

        public ScoreService(LudoContext db)
        {
            _db = db;
        }

        public void Add(Score score)
        {
            _db.Add(score);
            _db.SaveChanges();
        }

        public Score Get(int id)
        {
            return _db.Scores.Find(id);
        }

        public void Delete(int id)
        {
            var entity = _db.Scores.Find(id);
            if (entity == null) return;

            _db.Scores.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(int id, Score data)
        {
            var entity = _db.Scores.Find(id);
            if (entity == null) return;

            entity.Name = data.Name;
            entity.Points = data.Points;

            _db.SaveChanges();
        }

        public IList<Score> GetTop()
        {
            return (from s in _db.Scores orderby s.Points descending select s)
                .Take(3)
                .ToList();
        }

        public IList<Score> GetAll()
        {
            return (from s in _db.Scores orderby s.Points descending select s)
                .ToList();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Scores");
        }

        public void IncreaseScore(string name)
        {
            if (name == null) return;

            var score = _db.Scores.SingleOrDefault(s => s.Name == name);
            ;
            if (score != null)
                score.Points += 10;
            else
                _db.Add(new Score {Name = name, Points = 10});

            _db.SaveChanges();
        }
    }
}