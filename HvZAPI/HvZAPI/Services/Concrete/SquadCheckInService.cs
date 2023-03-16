using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class SquadCheckInService : ISquadCheckInService
    {
        private readonly HvZDbContext _context;
        public SquadCheckInService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<SquadCheckIn> CreateSquadCheckIn()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSquadCheckIn(int SquadCheckInId, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }

        public Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId)
        {
            return await _context.SquadChecksIns.Include(x => x.Squad).Where(x => x.SquadId == squadId).Where(x => x.Squad.GameId == gameId).ToListAsync();
        }

        public Task<SquadCheckIn> UpdateSquadCheckIn(SquadCheckIn SquadCheckIn, int gameId, int squadId)
        {
            throw new NotImplementedException();
        }
    }
}
