namespace HvZAPI.Models.DTOs.ChatMessageDTOs
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }
        public string Channel { get; set; }
        public string Sender { get; set; }
        public string Contents { get; set; }
    }
}
