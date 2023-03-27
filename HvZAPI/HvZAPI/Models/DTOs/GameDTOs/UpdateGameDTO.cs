namespace HvZAPI.Models.DTOs.GameDTOs
{
    public class UpdateGameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GameState { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float Radius { get; set; }
        public string Location { get; set; }
    }
}
