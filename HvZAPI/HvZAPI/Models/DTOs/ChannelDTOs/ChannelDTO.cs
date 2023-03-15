namespace HvZAPI.Models.DTOs.ChannelDTOs
{
    public class ChannelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public List<string> Messages { get; set; }
    }
}
