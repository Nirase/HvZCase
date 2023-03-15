using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChatMessageDTOs;

namespace HvZAPI.Services.Interfaces
{
    public interface IChatMessageService
    {
        /// <summary>
        /// Fetches all ChatMessages
        /// </summary>
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
        /// <param name="ChatMessage">ChatMessage to create</param>
        /// <param name="gameId">Id of game</param>
        /// <returns>Created ChatMessage entity</returns>
        Task<ChatMessage> CreateChatMessage(ChatMessage chatMessage, int gameId);

        /// <summary>
        /// Deletes an existing ChatMessage entity
        /// </summary>
        /// <param name="ChatMessageId">ChatMessage entity to delete</param>
        /// <param name="gameId">Game entity to delete from</param>
        Task DeleteChatMessage(int chatMessageId, int gameId);

        /// <summary>
        /// Updates a ChatMessage entity
        /// </summary>
        /// <param name="ChatMessage">Updated ChatMessage entity</param>
        /// <param name="gameId">Game id</param>
        /// <returns>Updated ChatMessage entity</returns>
        Task<ChatMessage> UpdateChatMessage(ChatMessage chatMessage, int gameId);
    }
}
