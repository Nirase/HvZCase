using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class SquadService : ISquadService
    {
        private readonly HvZDbContext _context;
        public SquadService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<Squad> CreateSquad(Squad Squad, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSquad(int id, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<Squad> GetSquadById(int id, int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Squad>> GetSquads(int gameId)
        {
            return await _context.Squads.Include(x => x.Players).Where(x => x.GameId == gameId).ToListAsync();
        }

        public Task<Squad> UpdateSquad(Squad Squad, int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
