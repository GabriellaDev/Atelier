using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AtelierShared.Models; 

namespace AtelierAPI.Data
{
    public class AtelierContext : DbContext
    {
        public AtelierContext(DbContextOptions<AtelierContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User>? Users { get; set; }
        public DbSet<InitiativePost>? InitiativePosts { get; set; }
        public DbSet<CategoryModel>? Category {get; set; }
        public object Categories { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseSqlite(
    optionsBuilder.Options.FindExtension<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>()?.ConnectionString 
    ?? throw new InvalidOperationException("No connection string configured.")
);


    // Suppress the pending model changes warning (use only during debugging)
    optionsBuilder.ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "hashed_password", // Replace with a hashed password
                    Role = "Admin",
                    Email = "316333@viauc.dk",
                    DateJoined = DateTime.UtcNow
                }
            );

            // Seed data for InitiativePost
            modelBuilder.Entity<InitiativePost>().HasData(
                new InitiativePost
                {
                    Id = 1,
                    Title = "Improve Blade Manufacturing Efficiency",
                    Content = "Implement automated robotic systems for precision cutting and assembly of wind turbine blades. This will reduce production time by 20% and minimize material waste.",
                    CategoryId = 3,
                    AuthorId = 1 // Link to the seeded User
                }
            );

            // Seed Categories
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Category = "Concept Development" },
                new CategoryModel { Id = 2, Category = "Design & Prototyping" },
                new CategoryModel { Id = 3, Category = "Manufacturing & Production" },
                new CategoryModel { Id = 4, Category = "Sustainability & Green Technology" }
            );
        }
    }
}
