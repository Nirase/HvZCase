using HvZAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Contexts
{
    public class HvZDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Kill> Kills { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }

        public HvZDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Mattias", LastName = "Smedman"},
                new User { Id = 2, FirstName = "Danielle", LastName = "Hamrin" },
                new User { Id = 3, FirstName = "Keman", LastName = "Nguyen" }
                );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "Base Game", Description = "A basic game of HvZ", StartDate = "2022-04-21", EndDate="2035-02-12", GameState = "Registration" },
                new Game { Id = 2, Name = "Playing Game", Description = "A basic currently running game of HvZ", StartDate="2021-12-01", EndDate="2022-04-23", GameState = "InProgress" },
                new Game { Id = 3, Name = "Completed Game", Description = "A basic completed game of HvZ", StartDate = "2020-01-01", EndDate ="2020-01-04", GameState = "Completed" }
                );

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, GameId = 1, UserId = 1, IsHuman = false, IsPatientZero = true, BiteCode = "base"},
                new Player { Id = 2, GameId = 1, UserId = 2, IsHuman = false, IsPatientZero = false, BiteCode = "bite" },
                new Player { Id = 3, GameId = 1, UserId = 3, IsHuman = true, IsPatientZero = false, BiteCode = "safe" },
                new Player { Id = 4, GameId = 2, UserId = 1, IsHuman = true, IsPatientZero = false, BiteCode = "base" },
                new Player { Id = 5, GameId = 2, UserId = 2, IsHuman = false, IsPatientZero = true, BiteCode = "bite" },
                new Player { Id = 6, GameId = 2, UserId = 3, IsHuman = true, IsPatientZero = false, BiteCode = "safe" },
                new Player { Id = 7, GameId = 3, UserId = 1, IsHuman = false, IsPatientZero = false, BiteCode = "base" },
                new Player { Id = 8, GameId = 3, UserId = 2, IsHuman = false, IsPatientZero = true, BiteCode = "bite" },
                new Player { Id = 9, GameId = 3, UserId = 3, IsHuman = false, IsPatientZero = false, BiteCode = "safe" }
                );
            modelBuilder.Entity<Kill>()
                .HasOne(k => k.Killer)
                .WithMany()
                .HasForeignKey(k => k.KillerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kill>().HasData(
                new Kill { Id = 1, GameId = 1, KillerId = 1, VictimId = 2, TimeOfDeath = "2023-03-06"},
                new Kill { Id = 2, GameId = 3, KillerId = 8, VictimId = 7, TimeOfDeath = "2023-03-01" },
                new Kill { Id = 3, GameId = 3, KillerId = 8, VictimId = 9, TimeOfDeath = "2023-03-30" }
                );
        }

    }
}
