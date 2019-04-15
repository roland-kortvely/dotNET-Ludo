using System.Collections.Generic;
using System.Linq;
using LudoLibrary.Database;
using LudoLibrary.Interfaces;
using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Services
{
    public class RatingService : IRatingService
    {
        private readonly LudoContext _db;

        public RatingService() : this(new LudoContext())
        {
        }

        public RatingService(LudoContext db)
        {
            _db = db;
        }

        public void Add(Rating rating)
        {
            _db.Add(rating);
            _db.SaveChanges();
        }

        public Rating Get(int id)
        {
            return _db.Ratings.Find(id);
        }

        public void Delete(int id)
        {
            var entity = _db.Ratings.Find(id);
            if (entity == null) return;

            _db.Ratings.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(int id, Rating data)
        {
            var entity = _db.Ratings.Find(id);
            if (entity == null) return;

            entity.Stars = data.Stars;
            entity.Content = data.Content;

            _db.SaveChanges();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
        }

        public IList<Rating> GetAll()
        {
            return (from s in _db.Ratings orderby s.Id descending select s)
                .ToList();
        }

        public float AverageRating()
        {
            var ratings = (from s in _db.Ratings select s.Stars).ToList();

            if (ratings.Count == 0) return 0.0f;

            var sum = 0.0f;

            ratings.ForEach(r => sum += r);

            return sum / ratings.Count;
        }

        public void Rate(int stars, string content)
        {
            if (stars < 0 || stars > 5) return;

            if (content == null) return;

            Add(new Rating
            {
                Stars = stars,
                Content = content
            });
        }
    }
}