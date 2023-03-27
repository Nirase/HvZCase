namespace HvZAPI.Models.DTOs.ChatMessageDTOs
{
    public class LightweightMessageDTO
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public int ChannelId { get; set; }
        public int SenderId { get; set; }
        public string Contents { get; set; }
    }
}
