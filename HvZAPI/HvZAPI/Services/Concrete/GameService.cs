using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class GameService : IGameService
    {
        public readonly HvZDbContext _context;
        public GameService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }
    }
}
