using HvZAPI.Contexts;
using HvZAPI.Exceptions;
using HvZAPI.Models;
using HvZAPI.Models.DTOs.ChatMessageDTOs;
using HvZAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PusherServer;

namespace HvZAPI.Services.Concrete
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly HvZDbContext _context;
        public ChatMessageService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<ChatMessage> CreateChatMessage(ChatMessage chatMessage, int gameId, string subject)
        {
            var sender = await _context.Players.Include(x => x.User).Where(x => x.Id == chatMessage.PlayerId).Where(x => x.User.KeycloakId == subject).FirstOrDefaultAsync();
            if (sender is null)
                throw new PlayerNotFoundException("Player not found");

            await _context.ChatMessages.AddAsync(chatMessage);
            await _context.SaveChangesAsync();
            var created = await GetChatMessageById(chatMessage.Id, gameId);
            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1567386",
              "e346b81befca052d8721",
              "10876182a6c82c619b0b",
              options);
            var result = await pusher.TriggerAsync(
              created.Channel.Name,
              "MessageRecieved",
              new { channel = created.Channel.Name, message = created.Contents, sender = sender.User.FirstName + " " + sender.User.LastName });
            return chatMessage;
        }

        public async Task DeleteChatMessage(int chatMessageId, int gameId)
        {
            var message = await GetChatMessageById(chatMessageId, gameId);
            _context.ChatMessages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatMessage> GetChatMessageById(int id, int gameId)
        {
            var message = await _context.ChatMessages.Include(x => x.Player).Include(x => x.Channel).FirstOrDefaultAsync(x => x.Id == id);
            if (message is null)
                throw new ChatMessageNotFoundException($"Message {id} not found");
            return message;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(int gameId)
        {
            return await _context.ChatMessages.Include(x => x.Player).Include(x => x.Channel).Where(x => x.Channel.GameId == gameId).ToListAsync();
        }
    }
}
