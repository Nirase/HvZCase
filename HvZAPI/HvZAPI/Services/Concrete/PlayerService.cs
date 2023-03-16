using HvZAPI.Contexts;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HvZAPI.Services.Concrete
{
    public class PlayerService: IPlayerService
    {
        private readonly HvZDbContext _context;
        public PlayerService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Player> AddPlayer(int gameId, Player player)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);
            if (game is null)
                throw new Exception("Game not found");

            //player.IsHuman = true;
            //player.IsPatientZero = false;
            //player.GameId= gameId;

            var users = await _context.Users.FirstOrDefaultAsync(x => x.Id == player.UserId);
            if(users is null)
                throw new Exception("User not found");
            var existingPlayers = await GetPlayers(gameId);
            foreach (var existingPlayer in existingPlayers)
            {
                if(existingPlayer.UserId == player.UserId)
                {
                    throw new Exception("Player already in game");
                }
            }
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task DeletePlayer(int gameId, int playerId)
        {
            var player = await GetPlayer(gameId, playerId);

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);
            if (game is null)
                throw new Exception("Game not found");
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetPlayer(int gameId, int playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == playerId && x.GameId == gameId);
            if (player is null)
                throw new Exception("Player Not Found");
            return player;
        }

        public async Task<IEnumerable<Player>> GetPlayers(int gameId)
        {
            return await _context.Players.Include(x => x.User).Where(x => x.GameId == gameId).ToListAsync();
        }

        public async Task<Player> UpdatePlayer(int gameId, Player player)
        {
            var foundPlayer = await GetPlayer(gameId, player.Id);
            if (foundPlayer.IsHuman && !player.IsHuman)
            {
                var kill = await _context.Kills.FirstOrDefaultAsync(x => x.VictimId == player.Id);
                if (kill is null)
                    throw new Exception("kill not found");

                var game = await _context.Games.Include(x => x.Kills).FirstOrDefaultAsync(x => x.Id == gameId);
                if (game is null)
                    throw new Exception("Game not found");

                var victim = await _context.Players.FirstOrDefaultAsync(x => x.Id == kill.VictimId);

                if (victim is null)
                    throw new Exception("Victim is null");
                game.Kills.Remove(kill);
                foundPlayer.IsHuman = player.IsHuman;
            }
            foundPlayer.IsPatientZero = player.IsPatientZero;
            await _context.SaveChangesAsync();
            return foundPlayer;
        }
    }
}
