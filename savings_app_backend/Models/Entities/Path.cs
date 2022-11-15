

namespace savings_app_backend.Models.Entities
{
    public class Path
    {
        public List<Visit> Visits { get; set; }
        public int Length { get; set; } = 0;
        public DateTime DepartureTime { get; set; }
        public double Duration { get; set; } = 0;

        public string? StartLocationPlaceId { get; set; }
    }
    
}
