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
    }
}
