using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChatMessageDTOs;

namespace HvZAPI.Services.Interfaces
{
    public interface IChatMessageService
    {
        /// <summary>
        /// Fetches all ChatMessages
        /// </summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Enumerable of ChatMessages</returns>
        Task<IEnumerable<ChatMessage>> GetChatMessages(int gameId);

        /// <summary>
        /// Fetches ChatMessage based on id
        /// </summary>
        /// <param name="id">ChatMessage Id to find</param>
        /// <param name="gameId">Game Id to search in</param>
        /// <returns>Found ChatMessage entity</returns>
        Task<ChatMessage> GetChatMessageById(int id, int gameId);

        /// <summary>
        /// Creates a new ChatMessage entity
        /// </summary>
        /// <param name="chatMessage">ChatMessage to create</param>
        /// <param name="gameId">Id of game</param>
        /// <param name="subject">Subject issuing the request</param>
        /// <returns>Created ChatMessage entity</returns>
        Task<ChatMessage> CreateChatMessage(ChatMessage chatMessage, int gameId, string subject);

        /// <summary>
        /// Deletes an existing ChatMessage entity
        /// </summary>
        /// <param name="chatMessageId">ChatMessage entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteChatMessage(int chatMessageId, int gameId);

    }
}
