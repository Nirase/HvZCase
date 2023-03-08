using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class KillService : IKillService
    {
        private readonly HvZDbContext _context;
        public KillService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Kill> CreateKill(int killerId, int gameId, string biteCode)
        {
            var victim = await _context.Players.FirstOrDefaultAsync(p => p.BiteCode== biteCode);
            if (victim is null)
                throw new Exception("Victim not found");

            var killer = await _context.Players.FirstOrDefaultAsync(p => p.Id == killerId);
            if (killer is null)
                throw new Exception("Killer not found");
            if (killer.IsHuman || !victim.IsHuman)
                throw new Exception("Invalid kill");

            var kill = new Kill { GameId = gameId, TimeOfDeath = DateTime.Now.ToString(), KillerId = killerId, VictimId = victim.Id };
            victim.IsHuman = false;
            _context.Kills.Add(kill);
            await _context.SaveChangesAsync();
            return kill;
        }

        public async Task<Kill> GetKillById(int id, int gameId)
        {
            var kill = await _context.Kills.Include(x => x.Victim).Include(x => x.Killer).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (kill is null)
                throw new Exception("Kill Not Found");
            return kill;
        }

        public async Task<IEnumerable<Kill>> GetKills(int gameId)
        {
            return await _context.Kills.Include(x => x.Victim).Include(x => x.Killer).Where(x => x.GameId == gameId).ToListAsync();
        }

    }
}
