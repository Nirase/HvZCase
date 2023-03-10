using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IMissionService
    {
        /// <summary>
        /// Fetches all Missions
        /// </summary>
        /// <returns>Enumerable of Missions</returns>
        Task<IEnumerable<Mission>> GetMissions();

        /// <summary>
        /// Fetches Mission based on id
        /// </summary>
        /// <param name="id">Mission Id to find</param>
        /// <returns>Found Mission entity</returns>
        Task<Mission> GetMissionById(int id);

        /// <summary>
        /// Creates a new Mission entity
        /// </summary>
        /// <param name="Mission">Mission to create</param>
        /// <returns>Created Mission entity</returns>
        Task<Mission> CreateMission(Mission Mission);

        /// <summary>
        /// Updates an existing Mission entity
        /// </summary>
        /// <param name="Mission">Mission entity to update to</param>
        /// <returns>Updated Mission entity</returns>
        Task<Mission> UpdateMission(Mission Mission);

        /// <summary>
        /// Deletes a Mission entity
        /// </summary>
        /// <param name="id">Id of Mission entity</param>
        Task DeleteMission(int id);
    }
}
