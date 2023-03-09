using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IKillService
    {

        /// <summary>
        /// Fetches all Kills
        /// </summary>
        /// <returns>Enumerable of Kills</returns>
        Task<IEnumerable<Kill>> GetKills(int gameId);

        /// <summary>
        /// Fetches Kill based on id
        /// </summary>
        /// <param name="id">Kill Id to find</param>
        /// <returns>Found Kill entity</returns>
        Task<Kill> GetKillById(int id, int gameId);

        /// <summary>
        /// Creates a new Kill entity
        /// </summary>
        /// <param name="killerId">Id of killer</param>
        /// <param name="gameId">Id of game</param>
        /// <param name="biteCode">Supplied bitecode</param>
        /// <returns>Created Kill entity</returns>
        Task<Kill> CreateKill(int killerId, int gameId, string biteCode);

        /// <summary>
        /// Deletes an existing Kill entity
        /// </summary>
        /// <param name="killId">Kill entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteKill(int killId, int gameId);

        /// <summary>
        /// Updates a Kill entity
        /// </summary>
        /// <param name="kill">Updated kill entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated kill entity</returns>
        Task<Kill> UpdateKill(Kill kill, int gameId);
    }
}
