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
        Task<Player> GetPlayer(int gameId, int playerId);

        /// <summary>
        /// Fetches a player in a game based on id
        /// </summary>
        /// <returns> Player entity </returns>
        Task<Player> AddPlayer(int gameId, Player player, int userId);

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
