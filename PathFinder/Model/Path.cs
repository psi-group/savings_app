using PathFinder.Services;

namespace PathFinder.Model
{
    public class Path
    {
        private GMPDistanceMatrix GMPDistanceMatrix { get; set; }

        public Path(GMPDistanceMatrix gMPDistanceMatrix, List<Visit> visits, string startLocationPlaceId)
        {
            GMPDistanceMatrix = gMPDistanceMatrix;
            Visits = visits;
            StartLocationPlaceId = startLocationPlaceId;
        }

        public Path(GMPDistanceMatrix gMPDistanceMatrix)
        {
            GMPDistanceMatrix = gMPDistanceMatrix;
        }


        public List<Visit> Visits { get; } = new List<Visit>();
        public int Length { get; set; } = 0;
        public DateTime DepartureTime { get; set; }
        public double Duration { get; set; } = 0;

        public string? StartLocationPlaceId { get; set; }

        public void AddVisit(Visit visit)
        {
            Visits.Add(visit);
            Length++;
        }

        public void RemoveVisit(Visit visit)
        {
            Visits.Remove(visit);
            Length--;
        }

        public Visit GetLastVisit()
        {
            return Visits[Visits.Count - 1];
        }

        public void ComputeDuration()
        {
            int drivingTimeOffset = 0;

            if (Visits == null)
                return ;

            if (Visits.Count < 2)
                return ;

            double duration = 0;

            for (int i = 0; i < Visits.Count - 1; i++)
            {
                int? drivingTime = 0;

                if(Visits[i].PlaceId == null)
                {
                    Duration = -1;
                    return;
                }

                if (!Visits[i].PlaceId.Equals(Visits[i + 1].PlaceId))
                {
                    drivingTime = GMPDistanceMatrix.GetDurationDepartAt(
                    Visits[i].PlaceId,
                    Visits[i + 1].PlaceId,
                    Visits[i].EndTime);

                    if (drivingTime == null)
                    {
                        this.Duration = -1;
                        return;
                    }
                        
                }

                if (Visits[i].EndTime.AddSeconds((double)(drivingTime + drivingTime * drivingTimeOffset / 100)) > Visits[i + 1].StartTime)
                {
                    this.Duration = -1;
                    return;
                }
                else
                {
                    duration += (Visits[i + 1].EndTime - Visits[i].EndTime).TotalSeconds;
                }
            }

            var initialDriveTime = GMPDistanceMatrix.GetDurationArriveBy(
                    StartLocationPlaceId,
                    Visits[0].PlaceId,
                    Visits[0].StartTime);

            if (initialDriveTime == null)
            {
                this.Duration = -1;
                return;
            }

            duration += (double)(initialDriveTime + initialDriveTime * drivingTimeOffset / 100) + (Visits[0].EndTime - Visits[0].StartTime).TotalSeconds;

            this.Duration = duration;

            DepartureTime = Visits[0].StartTime.AddSeconds((double)-initialDriveTime);

            return;

        }

    }
    
}
