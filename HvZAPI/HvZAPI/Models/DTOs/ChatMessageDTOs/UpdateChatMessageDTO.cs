namespace HvZAPI.Models.DTOs.ChatMessageDTOs
{
    public class UpdateChatMessageDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ChannelId { get; set; }
        public int PlayerId { get; set; }
        public string Contents { get; set; }
    }
}
