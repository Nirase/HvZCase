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

        public async Task<Game> CreateGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task DeleteGame(int id)
        {
            var game = await GetGameById(id);
            
            foreach(var kill in game.Kills) 
                _context.Kills.Remove(kill);
            
            game.Kills.Clear();
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = await _context.Games.Include(x => x.Players).Include(x => x.Kills).FirstOrDefaultAsync(x => x.Id == id);
            if (game is null)
                throw new Exception("Game Not Found");
            return game;
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _context.Games.Include(x => x.Players).Include(x => x.Kills).ToListAsync();
        }

        public async Task<Game> UpdateGame(Game game)
        {
            var foundGame = await GetGameById(game.Id);
            foundGame.GameState = game.GameState;
            await _context.SaveChangesAsync();
            return foundGame;
        }
    }
}
