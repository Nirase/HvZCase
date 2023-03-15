using HvZAPI.Models.DTOs.ChannelDTOs;

namespace HvZAPI.Models.DTOs.ChatMessageDTOs
{
    public class CreateChatMessageDTO
    {
        public int GameId { get; set; }
        public int ChannelId { get; set; }
        public int PlayerId { get; set; }
        public string Contents { get; set; }
    }
}
