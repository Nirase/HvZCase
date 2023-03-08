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

        public async Task<Kill> CreateKill(Kill kill)
        {
            _context.Kills.Add(kill);
            await _context.SaveChangesAsync();
            return kill;
        }

        public async Task<Kill> GetKillById(int id)
        {
            var kill = await _context.Kills.Include(x => x.Victim).Include(x => x.Killer).FirstOrDefaultAsync();
            if (kill is null)
                throw new Exception("Kill Not Found");
            return kill;
        }

        public async Task<IEnumerable<Kill>> GetKills()
        {
            return await _context.Kills.Include(x => x.Victim).Include(x => x.Killer).ToListAsync();
        }

    }
}
