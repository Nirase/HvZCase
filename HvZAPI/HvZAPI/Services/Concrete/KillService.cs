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

        public async Task DeleteKill(int killId, int gameId)
        {
            var kill = await GetKillById(killId, gameId);
            if (kill is null)
                throw new Exception("Kill not found");

            var game = await _context.Games.Include(x => x.Kills).FirstOrDefaultAsync(x => x.Id == gameId);

            if (game is null)
                throw new Exception("Game not found");

            var victim = await _context.Players.FirstOrDefaultAsync(x => x.Id == kill.VictimId);

            if (victim is null)
                throw new Exception("Victim is null");

            victim.IsHuman = true;
            game.Kills.Remove(kill);
            await _context.SaveChangesAsync();
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

        public async Task<Kill> UpdateKill(Kill kill, int gameId)
        {
            var currentKill = await GetKillById(kill.Id, gameId);

            if(kill.VictimId != currentKill.VictimId)
            {
                currentKill.Victim.IsHuman = true;
                var newVictim = await _context.Players.FirstOrDefaultAsync(x => x.Id == kill.VictimId);
                newVictim.IsHuman = false;
                currentKill.Victim= newVictim;
            }

            currentKill.KillerId = kill.KillerId;

            await _context.SaveChangesAsync();
            return currentKill;
        }
    }
}
