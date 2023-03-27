namespace HvZAPI.Models.DTOs.GameDTOs
{
    public class CreateGameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public float Radius { get; set; }
        public string Location { get; set; }

    }
}
