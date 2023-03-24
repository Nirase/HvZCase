using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface ISquadCheckInService
    {
        /// <summary>
        /// Fetches all SquadCheckIns
        /// </summary>
        /// <param name="gameId">Game entity to delete from</param>
        /// <param name="squadId">Squad to delete check in from</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Enumerable of SquadCheckIns</returns>
        Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId, string subject);

        /// <summary>
        /// Fetches SquadCheckIn based on id
        /// </summary>
        /// <param name="id">SquadCheckIn Id to find</param>
        /// <param name="gameId">Game entity to delete from</param>
        /// <param name="squadId">Squad to delete check in from</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Found SquadCheckIn entity</returns>
        Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId, string subject);

        /// <summary>
        /// Creates a new SquadCheckIn entity
        /// </summary>
        /// <param name="squadCheckIn">SquadCheckIn to create</param>
        /// <param name="gameId">Game entity to delete from</param>
        /// <param name="squadId">Squad to delete check in from</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Created SquadCheckIn entity</returns>
        Task<SquadCheckIn> CreateSquadCheckIn(SquadCheckIn squadCheckIn, int gameId, int squadId, string subject);

        /// <summary>
        /// Deletes an existing SquadCheckIn entity
        /// </summary>
        /// <param name="SquadCheckInId">SquadCheckIn entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        /// <param name="squadId">Squad to delete check in from</param>
        /// <param name="subject">Subject issuing the request</param>
        Task DeleteSquadCheckIn(int SquadCheckInId, int gameId, int squadId, string subject);

        
    }
}
