using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IMissionService
    {
        /// <summary>
        /// Fetches all Missions
        /// </summary>
        /// <returns>Enumerable of Missions</returns>
        Task<IEnumerable<Mission>> GetMissions(int gameId);

        /// <summary>
        /// Fetches Mission based on id
        /// </summary>
        /// <param name="id">Mission Id to find</param>
        /// <returns>Found Mission entity</returns>
        Task<Mission> GetMissionById(int id, int gameId);

        /// <summary>
        /// Creates a new Mission entity
        /// </summary>
        /// <returns>Created Mission entity</returns>
        Task<Mission> CreateMission(Mission mission);

        /// <summary>
        /// Deletes an existing Mission entity
        /// </summary>
        /// <param name="MissionId">Mission entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteMission(int MissionId, int gameId);

        /// <summary>
        /// Updates a Mission entity
        /// </summary>
        /// <param name="Mission">Updated Mission entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated Mission entity</returns>
        Task<Mission> UpdateMission(Mission Mission, int gameId);
    }
}
