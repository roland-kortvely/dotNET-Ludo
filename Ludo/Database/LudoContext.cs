using Ludo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ludo.Database
{
    public class LudoContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Database;Trusted_Connection=True;");
        }
    }
}