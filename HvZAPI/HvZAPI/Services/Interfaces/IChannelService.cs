using HvZAPI.Models;

namespace HvZAPI.Services.Interfaces
{
    public interface IChannelService
    {
        /// <summary>
        /// Fetches all Channels
        /// </summary>
        /// <returns>Enumerable of Channels</returns>
        Task<IEnumerable<Channel>> GetChannels(int gameId);

        /// <summary>
        /// Fetches Channel based on id
        /// </summary>
        /// <param name="id">Channel Id to find</param>
        /// <param name="gameId">Game Id to search in</param>
        /// <returns>Found Channel entity</returns>
        Task<Channel> GetChannelById(int id, int gameId);

        /// <summary>
        /// Fetches Channel based on id
        /// </summary>
        /// <param name="name">Channel name to find</param>
        /// <param name="gameId">Game Id to search in</param>
        /// <returns>Found Channel entity</returns>
        Task<Channel> GetChannelByName(string name, int gameId);

        /// <summary>
        /// Creates a new Channel entity
        /// </summary>
        /// <param name="channel">Channel to create</param>
        /// <param name="gameId">Id of game</param>
        /// <returns>Created Channel entity</returns>
        Task<Channel> CreateChannel(Channel channel, int gameId);

        /// <summary>
        /// Deletes an existing Channel entity
        /// </summary>
        /// <param name="channelId">Channel entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteChannel(int channelId, int gameId);

        /// <summary>
        /// Updates a Channel entity
        /// </summary>
        /// <param name="Channel">Updated Channel entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated Channel entity</returns>
        Task<Channel> UpdateChannel(Channel Channel, int gameId);
    }
}
