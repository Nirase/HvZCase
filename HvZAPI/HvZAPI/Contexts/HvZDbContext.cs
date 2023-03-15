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
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Squad> Squads { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public HvZDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Mattias", LastName = "Smedman"},
                new User { Id = 2, FirstName = "Danielle", LastName = "Hamrin" },
                new User { Id = 3, FirstName = "Keman", LastName = "Nguyen" }
                );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "Base Game", Description = "A basic game of HvZ", StartDate = "2022-04-21", EndDate="2035-02-12", Location = "Ryttersgatan 8, 242 31 Hörby", Radius = 1000, GameState = "Registration" },
                new Game { Id = 2, Name = "Playing Game", Description = "A basic currently running game of HvZ", StartDate="2021-12-01", Location = "Pärup, 242 91", EndDate="2022-04-23", Radius = 1000, GameState = "InProgress" },
                new Game { Id = 3, Name = "Completed Game", Description = "A basic completed game of HvZ", StartDate = "2020-01-01", Location = "Baltzarsgatan 41 A, 211 36 Malmö", Radius = 1000, EndDate ="2020-01-04", GameState = "Completed" }
                );

            modelBuilder.Entity<Mission>().HasData(
                new Mission { Id = 1, Name = "Raid!", Description = "Attack the human village", GameId = 1, StartDate = "2022-03-11", EndDate = "2022-03-12", VisibleToHumans = false, VisibleToZombies = true, Location = "Ryttersgatan 12, 242 31 Hörby" },
                new Mission { Id = 2, Name = "Loot!", Description = "Loot the local grocery store", GameId = 1, StartDate = "2022-03-11", EndDate = "2022-03-12", VisibleToHumans = true, VisibleToZombies = false, Location = "Ryttersgatan 3, 242 31 Hörby" },
                new Mission { Id = 3, Name = "Investigate!", Description = "A large noise was heard nearby, investigate the area to find out what caused it. Beware! The noise might have attracted zombies.", GameId = 1, StartDate = "2022-03-11", EndDate = "2022-03-12", VisibleToHumans = true, VisibleToZombies = true, Location = "Lågedammsgatan 23, 242 31 Hörby" }
                );
            modelBuilder.Entity<Squad>().HasData(
                new Squad { Id = 1, Name = "Cool cats", GameId = 1 }
                );
            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, GameId = 1, UserId = 1, IsHuman = false, IsPatientZero = true, BiteCode = "base", SquadId = 1},
                new Player { Id = 2, GameId = 1, UserId = 2, IsHuman = false, IsPatientZero = false, BiteCode = "bite", SquadId = 1 },
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
            modelBuilder.Entity<ChatMessage>()
                .HasOne(p => p.Player)
                .WithMany()
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kill>().HasData(
                new Kill { Id = 1, GameId = 1, KillerId = 1, VictimId = 2, TimeOfDeath = "2023-03-06", Location = "Ryttersgatan 8, 242 31 Hörby" },
                new Kill { Id = 2, GameId = 3, KillerId = 8, VictimId = 7, TimeOfDeath = "2023-03-01", Location = "Baltzarsgatan 43 A, 211 36 Malmö" },
                new Kill { Id = 3, GameId = 3, KillerId = 8, VictimId = 9, TimeOfDeath = "2023-03-30", Location = "Baltzarsgatan 40 A, 211 36 Malmö" }
                );

            modelBuilder.Entity<Channel>().HasData(
                new Channel { Id = 1, GameId = 1, Name = "Global"},
                new Channel { Id = 2, GameId = 1, Name = "Humans" },
                new Channel { Id = 3, GameId = 1, Name = "Zombies" }
                );


        }

    }
}
