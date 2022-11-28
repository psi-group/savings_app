using Microsoft.AspNetCore.Mvc;
using PathFinder.Model;
using PathFinder.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Path = PathFinder.Model.Path;

namespace PathFinder.Controllers
{
    [ApiController]
    [Route("visitPlanner")]
    public class DistanceMatrixController : ControllerBase
    {
        private GMPDistanceMatrix GMPDistanceMatrix { get; set; }
        private GMPGeocode GMPGeocode { get; set; }

        public DistanceMatrixController(GMPDistanceMatrix gMPDistanceMatrix, GMPGeocode gMPGeocode)
        {
            GMPDistanceMatrix = gMPDistanceMatrix;
            GMPGeocode = gMPGeocode;
        }

        [HttpGet("placeId")]
        public string? GetPlaceId([FromQuery] Address address)
        {
            return GMPGeocode.GetPlaceId(address);
        }

        [HttpGet("path")]
        public IEnumerable<Path> GetPaths(
            [FromQuery] string[] placeId,
            [FromQuery] int[] visitAmount,
            [FromQuery] Guid[] visitId,
            [FromQuery] DateTime[] startTime,
            [FromQuery] DateTime[] endTime, DateTime? minTime, DateTime? maxTime)
        {

            List<Location> locations = new List<Location>();

            int visitAmountIndex = 0;

            for (int i = 0; i < placeId.Length; ++i)
            {
                Location location = new Location();
                for(int j = 0; j < visitAmount[i]; ++j)
                {
                    location.AddVisit(new Visit(visitId[i], placeId[i], startTime[visitAmountIndex + j], endTime[visitAmountIndex + j]));
                    
                }
                visitAmountIndex = visitAmount[i];
                locations.Add(location);
            }

            {
                var visits = new List<Visit>();
                visits.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Purmaliai", "Pilaites", 7)), DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddMinutes(10)));
                visits.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Purmaliai", "Pilaites", 7)), DateTime.Now.AddDays(2).AddHours(4), DateTime.Now.AddDays(2).AddHours(4).AddMinutes(5)));


                var visits1 = new List<Visit>();
                visits1.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Tilzes", 19)), DateTime.Now.AddDays(2).AddHours(2), DateTime.Now.AddDays(2).AddHours(2).AddMinutes(7)));
                visits1.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Tilzes", 19)), DateTime.Now.AddDays(2).AddHours(0).AddMinutes(42), DateTime.Now.AddDays(2).AddHours(0).AddMinutes(50)));

                var visits2 = new List<Visit>();
                visits2.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Svajones", 33)), DateTime.Now.AddDays(2).AddHours(1), DateTime.Now.AddDays(2).AddHours(1).AddMinutes(4)));
                visits2.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Svajones", 33)), DateTime.Now.AddDays(2).AddHours(4).AddMinutes(15), DateTime.Now.AddDays(2).AddHours(4).AddMinutes(30)));
                visits2.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Svajones", 33)), DateTime.Now.AddDays(2).AddHours(2).AddMinutes(20), DateTime.Now.AddDays(2).AddHours(2).AddMinutes(30)));

                var visits3 = new List<Visit>();
                visits3.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Naikupes", 18)), DateTime.Now.AddDays(2).AddHours(0).AddMinutes(35), DateTime.Now.AddDays(2).AddHours(0).AddMinutes(45)));
                visits3.Add(new Visit(Guid.NewGuid(), GMPGeocode.GetPlaceId(new Address("Lithuania", "Klaipeda", "Naikupes", 18)), DateTime.Now.AddDays(2).AddHours(3).AddMinutes(40), DateTime.Now.AddDays(2).AddHours(3).AddMinutes(45)));

                locations.Clear();


                locations.Add(new Location(visits));
                locations.Add(new Location(visits1));
                locations.Add(new Location(visits2));
                locations.Add(new Location(visits3));
            }

            StringBuilder url = new StringBuilder();

            foreach(var location in locations)
            {
                url.Append("placeId=");
                url.Append(location.Visits[0].PlaceId);
                url.Append("&visitAmount=");
                url.Append(location.Visits.Count);
                foreach(var visit in location.Visits)
                {
                    url.Append("&startTime=");
                    url.Append(visit.StartTime);
                    url.Append("&endTime=");
                    url.Append(visit.EndTime);
                }
            }


            List<Path> paths = new List<Path>();
            Path currentPath = new Path(GMPDistanceMatrix);

            Address startAddress = new Address("Lithuania", "Klaipeda", "Alksnynes", 8);
            var startAddressPlaceId = GMPGeocode.GetPlaceId(startAddress);
            
            helper(locations, new HashSet<Location>(), currentPath, paths, startAddressPlaceId);

            var a = "hello";

            foreach(var path in paths)
            {
                path.ComputeDuration();
            }

            ((List<Path>)paths).Sort((a, b) => a.Duration.CompareTo(b.Duration));

            List<Path> goodPaths = new List<Path>();

            foreach (var path in paths)
            {
                if (path.Duration != -1)
                    goodPaths.Add(path);
            }

            return goodPaths;
        }

        private void helper(List<Location> Locations, HashSet<Location> visited, Path currentPath, List<Path> paths, string startAddressPlaceId)
        {
            if (currentPath.Length == Locations.Count)
            {
                paths.Add(new Path(GMPDistanceMatrix, new List<Visit>(currentPath.Visits), startAddressPlaceId));
            }

            foreach (var location in Locations)
            {
                if (!visited.Contains(location))
                {
                    foreach (var visit in location.Visits)
                    {
                        if (currentPath.Length < 1 || visit.StartTime > currentPath.GetLastVisit().EndTime)
                        {
                            currentPath.AddVisit(visit);
                            visited.Add(location);
                            helper(Locations, visited, currentPath, paths, startAddressPlaceId);
                            currentPath.RemoveVisit(visit);
                            visited.Remove(location);
                        }
                    }
                }

            }
        }
    }
}