namespace HvZAPI.Services.Interfaces
{
    public interface ISquadCheckInService
    {
        /// <summary>
        /// Fetches all SquadCheckIns
        /// </summary>
        /// <returns>Enumerable of SquadCheckIns</returns>
        Task<IEnumerable<SquadCheckIn>> GetSquadCheckIns(int gameId, int squadId);

        /// <summary>
        /// Fetches SquadCheckIn based on id
        /// </summary>
        /// <param name="id">SquadCheckIn Id to find</param>
        /// <returns>Found SquadCheckIn entity</returns>
        Task<SquadCheckIn> GetSquadCheckInById(int id, int gameId, int squadId);

        /// <summary>
        /// Creates a new SquadCheckIn entity
        /// </summary>
        /// <returns>Created SquadCheckIn entity</returns>
        Task<SquadCheckIn> CreateSquadCheckIn();

        /// <summary>
        /// Deletes an existing SquadCheckIn entity
        /// </summary>
        /// <param name="SquadCheckInId">SquadCheckIn entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteSquadCheckIn(int SquadCheckInId, int gameId, int squadId);

        /// <summary>
        /// Updates a SquadCheckIn entity
        /// </summary>
        /// <param name="SquadCheckIn">Updated SquadCheckIn entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated SquadCheckIn entity</returns>
        Task<SquadCheckIn> UpdateSquadCheckIn(SquadCheckIn SquadCheckIn, int gameId, int squadId);
    }
}
