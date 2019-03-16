using System;
using System.Collections;
using System.Linq;
using Ludo.Database;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace Ludo.Services
{
    public class RatingService : IRatingService
    {
        public void Add(Rating rating)
        {
            var db = new LudoContext();

            db.Add(rating);
            db.SaveChanges();
        }

        public void Clear()
        {
            var db = new LudoContext();

            db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
        }

        public IList GetAll()
        {
            var db = new LudoContext();

            return (from s in db.Ratings orderby s.Id descending select s)
                .ToList();
        }

        public float AverageRating()
        {
            var db = new LudoContext();

            var ratings = (from s in db.Ratings select s.Stars).ToList();

            if (ratings.Count == 0)
            {
                return 0.0f;
            }

            var sum = 0.0f;

            ratings.ForEach(r => sum += r);

            return sum / ratings.Count;
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