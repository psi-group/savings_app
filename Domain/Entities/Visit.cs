namespace Domain.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public string PlaceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        private Visit()
        {

        }

        public Visit(string placeId, DateTime startTime, DateTime endTime)
        {
            PlaceId = placeId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
