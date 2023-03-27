using HvZAPI.Models;
using HvZAPI.Models.DTOs.KillDTOs;

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
        /// <param name="gameId">Game id to search within</param>
        /// <returns>Found Kill entity</returns>
        Task<Kill> GetKillById(int id, int gameId);

        /// <summary>
        /// Creates a new Kill entity
        /// </summary>
        /// <param name="kill">Kill to create</param>
        /// <param name="gameId">Id of game</param>
        /// <param name="biteCode">Supplied bitecode</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Created Kill entity</returns>
        Task<Kill> CreateKill(Kill kill, int gameId, string biteCode, string subject);

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
