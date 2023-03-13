using HvZAPI.Models;

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
        Task<IEnumerable<Player>> GetPlayer(int gameId, int playerId);

        /// <summary>
        /// Fetches a player in a game based on id
        /// </summary>
        /// <returns> Player entity </returns>
        Task<IEnumerable<Player>> AddPlayer(int gameId, int playerId);
    }
}
