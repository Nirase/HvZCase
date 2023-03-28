using HvZAPI.Models;
using System.Security.Claims;

namespace HvZAPI.Services.Interfaces
{
    public interface IGameService
    {

        /// <summary>
        /// Fetches all games
        /// </summary>
        /// <returns>Enumerable of games</returns>
        Task<IEnumerable<Game>> GetGames(string subject, List<Claim> roles);

        /// <summary>
        /// Fetches game based on id
        /// </summary>
        /// <param name="id">Game Id to find</param>
        /// <returns>Found game entity</returns>
        Task<Game> GetGameById(int id);

        /// <summary>
        /// Creates a new game entity
        /// </summary>
        /// <param name="game">Game to create</param>
        /// <returns>Created game entity</returns>
        Task<Game> CreateGame(Game game);

        /// <summary>
        /// Updates an existing game entity
        /// </summary>
        /// <param name="game">Game entity to update to</param>
        /// <returns>Updated game entity</returns>
        Task<Game> UpdateGame(Game game);

        /// <summary>
        /// Deletes a game entity
        /// </summary>
        /// <param name="id">Id of game entity</param>
        /// <returns></returns>
        Task DeleteGame(int id);
    }
}
