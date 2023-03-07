using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IGameService
    {

        /// <summary>
        /// Fetches all games
        /// </summary>
        /// <returns>Enumerable of games</returns>
        Task<IEnumerable<Game>> GetGames();

        /// <summary>
        /// Fetches game based on id
        /// </summary>
        /// <param name="id">Game Id to find</param>
        /// <returns>Found game entity</returns>
        Task<Game> GetGameById(int id);
    }
}
