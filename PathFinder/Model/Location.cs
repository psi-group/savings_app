namespace PathFinder.Model
{
    public class Location
    {
        public List<Visit> Visits { get; } = new List<Visit>();

        public Location(List<Visit> visits)
        {
            Visits = visits;
        }

        public Location()
        {
        }

        public void AddVisit(Visit visit)
        {
            Visits.Add(visit);
        }

    }
}
