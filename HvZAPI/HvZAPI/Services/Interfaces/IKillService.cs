using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IKillService
    {

        /// <summary>
        /// Fetches all Kills
        /// </summary>
        /// <returns>Enumerable of Kills</returns>
        Task<IEnumerable<Kill>> GetKills();

        /// <summary>
        /// Fetches Kill based on id
        /// </summary>
        /// <param name="id">Kill Id to find</param>
        /// <returns>Found Kill entity</returns>
        Task<Kill> GetKillById(int id);

        /// <summary>
        /// Creates a new Kill entity
        /// </summary>
        /// <param name="Kill">Kill to create</param>
        /// <returns>Created Kill entity</returns>
        Task<Kill> CreateKill(Kill Kill);
    }
}
