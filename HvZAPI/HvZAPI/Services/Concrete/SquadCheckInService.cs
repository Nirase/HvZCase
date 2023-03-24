using HvZAPI.Contexts;
using HvZAPI.Exceptions;
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

        public async Task<SquadCheckIn> CreateSquadCheckIn(SquadCheckIn squadCheckIn, int gameId, int squadId, string subject)
        {
            var issuer = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).Where(x => x.SquadId == squadId).FirstOrDefaultAsync();
            if (issuer is null)
                throw new SubjectDoesNotMatchException("Subject does not match the request");
            _context.SquadChecksIns.Add(squadCheckIn);
            await _context.SaveChangesAsync();
            return squadCheckIn;
        }

        public async Task DeleteSquadCheckIn(int id, int gameId, int squadId, string subject)
        {
            var issuer = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).Where(x => x.SquadId == squadId).FirstOrDefaultAsync();
            if (issuer is null)
                throw new SubjectDoesNotMatchException("Subject does not match the request");
            var foundCheckIn = await GetSquadCheckInById(id, gameId, squadId, subject);
            if(foundCheckIn is null)
                throw new SquadCheckInNotFoundException("SquadCheckIn not found");
            _context.SquadChecksIns.Remove(foundCheckIn);
            await _context.SaveChangesAsync();
        }

        public async Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId, string subject)
        {
            var issuer = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).Where(x => x.SquadId == squadId).FirstOrDefaultAsync();
            if (issuer is null)
                throw new SubjectDoesNotMatchException("Subject does not match the request");
            var checkIn = await _context.SquadChecksIns.Include(x => x.Squad).Where(x => x.SquadId == squadId).Where(x => x.Squad.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (checkIn is null)
                throw new SquadCheckInNotFoundException("SquadCheckIn not found");
            return checkIn;
        }

        public async Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId, string subject)
        {
            var issuer = await _context.Players.Include(x => x.User).Where(x => x.User.KeycloakId == subject).Where(x => x.SquadId == squadId).FirstOrDefaultAsync();
            if (issuer is null)
                throw new SubjectDoesNotMatchException("Subject does not match the request");
            return await _context.SquadChecksIns.Include(x => x.Squad).Where(x => x.SquadId == squadId).Where(x => x.Squad.GameId == gameId).ToListAsync();
        }

    }
}
