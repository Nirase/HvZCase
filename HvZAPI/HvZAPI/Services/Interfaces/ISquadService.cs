using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface ISquadService
    {
        /// <summary>
        /// Fetches all Squads
        /// <param name="gameId">Game id to fetch from</param>
        /// </summary>
        /// <returns>Enumerable of Squads</returns>
        Task<IEnumerable<Squad>> GetSquads(int gameId);


        /// <summary>
        /// Fetches Squad based on id
        /// </summary>
        /// <param name="id">Squad Id to find</param>
        /// <param name="gameId">Game id</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Found Squad entity</returns>
        Task<Squad> GetSquadById(int id, int gameId, string subject);

        /// <summary>
        /// Creates a new Squad entity
        /// </summary>
        /// <param name="Squad">Squad to create</param>
        /// <param name="creator">Creator id</param>
        /// <param name="gameId">Game id</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Created Squad entity</returns>
        Task<Squad> CreateSquad(Squad Squad, int gameId, int creator, string subject);

        /// <summary>
        /// Updates an existing Squad entity
        /// </summary>
        /// <param name="Squad">Squad entity to update to</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated Squad entity</returns>
        Task<Squad> UpdateSquad(Squad Squad, int gameId);

        /// <summary>
        /// Deletes a Squad entity
        /// </summary>
        /// <param name="id">Id of Squad entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns></returns>
        Task DeleteSquad(int id, int gameId);
        /// <summary>
        /// Joins an existing squad
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="squadId">Squad id</param>
        /// <param name="playerId">Player id joining</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Updated squad with player in it</returns>
        Task<Squad> JoinSquad(int gameId, int squadId, int playerId, string subject);
        /// <summary>
        /// Leaves an existing squad
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <param name="squadId">Squad id</param>
        /// <param name="playerId">Player id joining</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Updated squad without player in it</returns>
        Task<Squad> LeaveSquad(int gameId, int squadId, int playerId, string subject);
    }
}
