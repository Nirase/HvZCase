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
                new User { Id = 1, first_name = "Mattias", last_name = "Smedman"},
                new User { Id = 2, first_name = "Danielle", last_name = "Hamrin" },
                new User { Id = 3, first_name = "Keman", last_name = "Nguyen" }
                );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "Base Game", Description = "A basic game of HvZ", GameState = "Registration" },
                new Game { Id = 2, Name = "Playing Game", Description = "A basic currently running game of HvZ", GameState = "InProgress" },
                new Game { Id = 3, Name = "Completed Game", Description = "A basic completed game of HvZ", GameState = "Completed" }
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


            modelBuilder.Entity<Kill>().HasData(
                new Kill { Id = 1, GameId = 1, KillerId = 1, VictimId = 2, TimeOfDeath = "2023-03-06"},
                new Kill { Id = 2, GameId = 3, KillerId = 2, VictimId = 1, TimeOfDeath = "2023-03-01" },
                new Kill { Id = 3, GameId = 3, KillerId = 2, VictimId = 3, TimeOfDeath = "2023-03-30" }
                );
        }

    }
}
