using HvZAPI.Models.DTOs.ChatMessageDTOs;

namespace HvZAPI.Models.DTOs.ChannelDTOs
{
    public class DetailedChannelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public List<ChatMessageDTO> Messages { get; set; }
    }
}
