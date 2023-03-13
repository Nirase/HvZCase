using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;

namespace HvZAPI.Services.Concrete
{
    public class PlayerService: IPlayerService
    {
        private readonly HvZDbContext _context;
        public PlayerService(HvZDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Player>> AddPlayer(int gameId, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetPlayer(int gameId, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Player>> GetPlayers(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
