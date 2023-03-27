using HvZAPI.Models;
using System.Security.Claims;

namespace HvZAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        /// <summary>
        /// Fetches all Players
        /// </summary>
        /// <returns>Enumerable of Players</returns>
        Task<IEnumerable<Player>> GetPlayers(int gameId);

        /// <summary>
        /// Fetches a player in a game based on id
        /// </summary>
        /// <returns> Player entity </returns>
        Task<Player> GetPlayer(int gameId, int playerId, string subject, List<Claim> roles);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId">Game to add player to</param>
        /// <param name="player">Player to add</param>
        /// <param name="subject">Subject issuing request</param>
        /// <param name="roles">Subject roles</param>
        /// <returns></returns>
        Task<Player> AddPlayer(int gameId, Player player, string subject, List<Claim> roles);

        /// <summary>
        /// Fetches a player in a game based on id
        /// </summary>
        /// <returns> Player entity </returns>
        Task<Player> UpdatePlayer(int gameId, Player player);

        /// <summary>
        /// deketes a player from a game based on id
        /// </summary>
        /// <returns> deletes player entity </returns>
        Task DeletePlayer(int gameId, int playerId);
    }
}
