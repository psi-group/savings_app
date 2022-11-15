namespace savings_app_backend.Models.Entities
{
    public class Visit
    {
        
        /*public Visit()
        {

        }

        public Visit(string placeId, DateTime startTime, DateTime endTime)
        {
            this.PlaceId = placeId;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }*/

        public Guid Id { get; set; }
        public string PlaceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
