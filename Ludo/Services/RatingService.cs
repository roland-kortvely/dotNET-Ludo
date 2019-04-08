using System.Collections.Generic;
using System.Linq;
using Ludo.Database;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class RatingService : IRatingService
    {
        public void Add(Rating rating)
        {
            using (var db = new LudoContext())
            {
                db.Add(rating);
                db.SaveChanges();
            }
        }

        public Rating Get(int id)
        {
            using (var db = new LudoContext())
            {
                return db.Ratings.Find(id);
            }
        }

        public void Delete(int id)
        {
            using (var db = new LudoContext())
            {
                var entity = db.Ratings.Find(id);
                if (entity == null)
                {
                    return;
                }

                db.Ratings.Remove(entity);
                db.SaveChanges();
            }
        }

        public void Update(int id, Rating data)
        {
            using (var db = new LudoContext())
            {
                var entity = db.Ratings.Find(id);
                if (entity == null)
                {
                    return;
                }

                entity.Stars = data.Stars;
                entity.Content = data.Content;

                db.SaveChanges();
            }
        }

        public void Clear()
        {
            using (var db = new LudoContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
            }
        }

        public IList<Rating> GetAll()
        {
            using (var db = new LudoContext())
            {
                return (from s in db.Ratings orderby s.Id descending select s)
                    .ToList();
            }
        }

        public float AverageRating()
        {
            using (var db = new LudoContext())
            {
                var ratings = (from s in db.Ratings select s.Stars).ToList();

                if (ratings.Count == 0)
                {
                    return 0.0f;
                }

                var sum = 0.0f;

                ratings.ForEach(r => sum += r);

                return sum / ratings.Count;
            }
        }

        public void Rate(int stars, string content)
        {
            if (stars < 0 || stars > 5)
            {
                return;
            }

            if (content == null)
            {
                return;
            }

            Add(new Rating
            {
                Stars = stars,
                Content = content
            });
        }
    }
}