using System.Collections;
using System.Linq;
using Ludo.Entities;
using Ludo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Services
{
    public class RatingService : IRatingService
    {
        public void Add(Rating rating)
        {
            var db = Game.Instance.DB;

            db.Add(rating);
            db.SaveChanges();
        }

        public void Clear()
        {
            var db = Game.Instance.DB;

            db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
        }

        public IList GetAll()
        {
            var db = Game.Instance.DB;

            return (from s in db.Ratings orderby s.Id descending select s)
                .ToList();
        }

        public float AverageRating()
        {
            return 0.0f;
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