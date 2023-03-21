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

        public async Task<Squad> CreateSquad(Squad squad, int gameId, int creatorId)
        {
            var creator = await _context.Players.FindAsync(creatorId);
            if (creator is null)
                throw new Exception("Player not found");
            if (creator.SquadId != null)
                throw new Exception("Player already in a squad");
            var foundSquad = await GetSquadByName(squad.Name, gameId);
            if(foundSquad != null)
                throw new Exception($"Squad with name {squad.Name} already exists");

            await _context.Squads.AddAsync(squad);
            await _context.SaveChangesAsync();
            creator.SquadId = squad.Id;
            await _context.SaveChangesAsync();
            return squad;
        }

        public async Task DeleteSquad(int id, int gameId)
        {
            var foundSquad = await GetSquadById(id, gameId);
            if (foundSquad == null)
                throw new Exception("Squad not found");
            _context.Squads.Remove(foundSquad);
            await _context.SaveChangesAsync();
        }

        public async Task<Squad> GetSquadById(int id, int gameId)
        {
            var squad = await _context.Squads.Include(x => x.Players).Include(x => x.SquadCheckIns).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Id == id);
            if (squad is null)
                throw new Exception("Squad not found");
            return squad;
        }

        public async Task<Squad?> GetSquadByName(string name, int gameId)
        {
            var squad = await _context.Squads.Include(x => x.Players).Include(x => x.SquadCheckIns).Where(x => x.GameId == gameId).FirstOrDefaultAsync(x => x.Name == name);
            if (squad != null)
                throw new Exception($"Squad with name {squad.Name} already exists");
            return squad;
        }

        public async Task<IEnumerable<Squad>> GetSquads(int gameId)
        {
            return await _context.Squads.Include(x => x.Players).Include(x => x.SquadCheckIns).Where(x => x.GameId == gameId).ToListAsync();
        }

        public async Task<Squad> JoinSquad(int gameId, int squadId, int playerId)
        {
            var foundPlayer = await _context.Players.Include(x => x.Squad).FirstOrDefaultAsync(x=> x.Id == playerId);
            if (foundPlayer is null)
                throw new Exception("Player not found");
            if (foundPlayer.Squad != null)
                throw new Exception("Player is already in a squad");
            
            var foundSquad = await _context.Squads.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == squadId);
            if (foundSquad is null)
                throw new Exception("Squad not found");
            
            foundPlayer.SquadId = squadId;
            foundSquad.Players.Add(foundPlayer);
            await _context.SaveChangesAsync();
            return foundSquad;
        }

        public async Task<Squad> LeaveSquad(int gameId, int squadId, int playerId)
        {
            var foundPlayer = await _context.Players.Include(x => x.Squad).FirstOrDefaultAsync(x => x.Id == playerId);
            if (foundPlayer is null)
                throw new Exception("Player not found");
            if (foundPlayer.Squad is null)
                throw new Exception("Player is not in a squad");
            if (foundPlayer.Squad.Id != squadId)
                throw new Exception("Player is trying to leave a squad they are not in");
            var foundSquad = await _context.Squads.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == squadId);
            if (foundSquad is null)
                throw new Exception("Squad not found");

            foundPlayer.SquadId = null;
            foundSquad.Players.Remove(foundPlayer);
            await _context.SaveChangesAsync();
            return foundSquad;
        }

        public async Task<Squad> UpdateSquad(Squad Squad, int gameId)
        {
            var foundGame = await GetSquadById(Squad.Id, gameId);
            foundGame.Name = Squad.Name;
            await _context.SaveChangesAsync();
            return foundGame;
        }
    }
}
