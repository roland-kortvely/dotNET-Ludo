using LudoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoLibrary.Database
{
    public class LudoContext : DbContext
    {
        public LudoContext()
        {
        }

        public LudoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Score> Scores { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Users);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=Database;Trusted_Connection=True;"
                );
        }
    }
}