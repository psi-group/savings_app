namespace PathFinder.Model
{
    public class Visit
    {
        

        public Visit(Guid id, string placeId, DateTime startTime, DateTime endTime)
        {
            this.Id = id;
            this.PlaceId = placeId;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public Guid Id { get; set; }
        public string PlaceId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
    }
}
