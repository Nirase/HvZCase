using HvZAPI.Contexts;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Services.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HvZAPI.Services.Concrete
{
    public class PlayerService : IPlayerService
    {
        private readonly HvZDbContext _context;
        public PlayerService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Player> AddPlayer(int gameId, Player player, string subject, List<Claim> roles)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);
            if (game is null)
                throw new GameNotFoundException("Game not found");
            var admin = roles.Where(x => x.Value == "admin").FirstOrDefault();
            
            var users = await _context.Users.FirstOrDefaultAsync(x => x.Id == player.UserId);
            if (users is null)
                throw new UserNotFoundException($"User {player.UserId} not found");
            var existingPlayers = await GetPlayers(gameId);
            foreach (var existingPlayer in existingPlayers)
            {
                if (existingPlayer.UserId == player.UserId)
                    throw new PlayerAlreadyInGameException("Player already in game");
            }
             
            var issuer = await _context.Users.Where(x => x.KeycloakId == subject).FirstOrDefaultAsync();
            if ((issuer is null || issuer.Id != users.Id) && admin == null)
                throw new SubjectDoesNotMatchException("Subject does not match request");
            _context.Players.Add(player);
            game.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task DeletePlayer(int gameId, int playerId)
        {
            var player = await _context.Players.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == playerId && x.GameId == gameId);

            if (player is null)
                throw new PlayerNotFoundException("Player not found");
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);

            if (game is null)
                throw new GameNotFoundException("Game not found");

            var kills = await _context.Kills.Where(x => x.KillerId == playerId || x.VictimId == playerId).ToListAsync();
            foreach (var kill in kills)
                _context.Kills.Remove(kill);
            await _context.SaveChangesAsync();
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player> GetPlayer(int gameId, int playerId, string subject, List<Claim> roles)
        {
            var player = await _context.Players.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == playerId && x.GameId == gameId);
            if (player is null)
                throw new PlayerNotFoundException("Player Not Found");
            if (roles.Where(x => x.Value == "admin").FirstOrDefault() != null)
                return player;
            if (player.User.KeycloakId != subject)
                throw new SubjectDoesNotMatchException("Subject does not have permissions to get value");
            return player;
        }

        public async Task<IEnumerable<Player>> GetPlayers(int gameId)
        {
            return await _context.Players.Include(x => x.User).Where(x => x.GameId == gameId).ToListAsync();
        }

        public async Task<Player> UpdatePlayer(int gameId, Player player)
        {
            var foundPlayer = await _context.Players.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == player.Id && x.GameId == gameId);
            if (foundPlayer == null)
                throw new PlayerNotFoundException("Player not found");
            
            if (player.IsHuman == true && foundPlayer.IsHuman == false)
            {
                var kill = await _context.Kills.FirstOrDefaultAsync(x => x.VictimId == player.Id);


                var game = await _context.Games.Include(x => x.Kills).FirstOrDefaultAsync(x => x.Id == gameId);
                if (kill != null)
                {
                    game.Kills.Remove(kill);
                }
            }
            if (player.SquadId != null)
            {

                var squad = await _context.Squads.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == player.SquadId);
                if (squad != null && squad.GameId == gameId)
                {
                    if (squad.Players.Contains(foundPlayer))
                    {
                        foundPlayer.IsHuman = player.IsHuman;
                        foundPlayer.IsPatientZero = player.IsPatientZero;
                    }
                    else
                    {
                        var oldSquad = await _context.Squads.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == foundPlayer.SquadId);
                        if (oldSquad != null)
                        {
                            oldSquad.Players.Remove(foundPlayer);
                        }

                        foundPlayer.IsHuman = player.IsHuman;
                        foundPlayer.IsPatientZero = player.IsPatientZero;
                        foundPlayer.SquadId = player.SquadId;
                        squad.Players.Add(foundPlayer);
                    }


                }
                else
                {
                    throw new SquadNotFoundException("Squad not found");
                }
            }
            else
            {
                var oldSquad = await _context.Squads.FirstOrDefaultAsync(x => x.Id == foundPlayer.SquadId);
                if (oldSquad != null)
                {
                    oldSquad.Players.Remove(foundPlayer);
                }
                
                foundPlayer.SquadId = player.SquadId;
                foundPlayer.IsHuman = player.IsHuman;
                foundPlayer.IsPatientZero = player.IsPatientZero;
            }

            await _context.SaveChangesAsync();
            return foundPlayer;
        }
    }
}
