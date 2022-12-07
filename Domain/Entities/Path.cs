namespace Domain.Entities
{
    public class Path
    {
        public List<Visit> Visits { get; set; }
        public int Length { get; set; }
        public DateTime DepartureTime { get; set; }
        public double Duration { get; set; }
        public string? StartLocationPlaceId { get; set; }
    }
    
}
