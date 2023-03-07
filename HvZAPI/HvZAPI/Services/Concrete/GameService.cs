using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class GameService : IGameService
    {
        private readonly HvZDbContext _context;
        public GameService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = await _context.Games.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == id);
            if (game is null)
                throw new Exception("Game Not Found");
            return game;
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }
    }
}
