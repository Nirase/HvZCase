using HvZAPI.Contexts;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            var global = new Channel { GameId = game.Id, Name = "Global" };
            var humans = new Channel { GameId = game.Id, Name = "Humans" };
            var zombies = new Channel { GameId = game.Id, Name = "Zombies" };
            _context.Channels.Add(global);
            _context.Channels.Add(humans);
            _context.Channels.Add(zombies);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task DeleteGame(int id)
        {
            var game = await GetGameById(id);
            
            if(game is null)
                throw new GameNotFoundException($"Game {id} not found");

            foreach(var kill in game.Kills) 
                _context.Kills.Remove(kill);
            
            game.Kills.Clear();
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game> GetGameById(int id)
        {
            var game = await _context.Games.Include(x => x.Players).ThenInclude(x => x.User).Include(x => x.Kills).Include(x => x.Missions).FirstOrDefaultAsync(x => x.Id == id);
            if (game is null)
                throw new GameNotFoundException($"Game {id} Not Found");
            return game;
        }

        public async Task<IEnumerable<Game>> GetGames(string subject, List<Claim> roles)
        {
            if(roles.Where(x => x.Value == "admin").ToList().Count > 0)
                return await _context.Games.Include(x => x.Players).ThenInclude(x => x.User).Include(x => x.Kills).Include(x => x.Missions).ToListAsync();

            if(subject == null)
                return await _context.Games.Include(x => x.Players).ThenInclude(x => x.User).Include(x => x.Kills).Include(x => x.Missions).Where(x => x.GameState == "Registration").ToListAsync();

            return await _context.Games.Include(x => x.Players).ThenInclude(x => x.User).Include(x => x.Kills).Include(x => x.Missions).Where(x => x.GameState == "Registration" || (x.Players.Where(x => x.User.KeycloakId == subject).ToList().Count > 0 && x.GameState == "InProgress")).ToListAsync();
        }

        public async Task<Game> UpdateGame(Game game)
        {
            var foundGame = await GetGameById(game.Id);
            foundGame.GameState = game.GameState;
            foundGame.StartDate = game.StartDate;
            foundGame.EndDate = game.EndDate;
            foundGame.Name = game.Name;
            foundGame.Description = game.Description;
            foundGame.Radius = game.Radius;
            foundGame.Location = game.Location;
            await _context.SaveChangesAsync();
            return foundGame;
        }
    }
}
