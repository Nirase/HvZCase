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

        public async Task DeleteSquadCheckIn(int id, int gameId, int squadId)
        {
            var foundCheckIn = await GetSquadCheckInById(id, gameId, squadId);
            if(foundCheckIn is null)
                throw new Exception("SquadCheckIn not found");
            _context.SquadChecksIns.Remove(foundCheckIn);
            await _context.SaveChangesAsync();
        }

        public async Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId)
        {
            var checkIn = await _context.SquadChecksIns.Include(x => x.Squad).Where(x => x.SquadId == squadId).Where(x => x.Squad.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (checkIn is null)
                throw new Exception("SquadCheckIn not found");
            return checkIn;
        }

        public async Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId)
        {
            return await _context.SquadChecksIns.Include(x => x.Squad).Where(x => x.SquadId == squadId).Where(x => x.Squad.GameId == gameId).ToListAsync();
        }

    }
}
