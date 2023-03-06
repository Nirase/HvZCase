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

    }
}
