using System.Net.Http.Headers;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathFinderController : ControllerBase
    {
        private readonly SavingsAppContext _context;

        public PathFinderController(SavingsAppContext context)
        {
            _context = context;
        }

        private string? GetPlaceId(Address address)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7251/visitplanner/placeid");

            string urlParameters = "?country=" + address.Country + "&city=" + address.City + "&streetName=" + address.StreetName +
                "&houseNumber=" + address.HouseNumber + "&appartmentNumber" + address.AppartmentNumber + "&postalCode=" +
                address.PostalCode;

            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            string? placeId = null;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    placeId = response.Content.ToString();
                }
                catch (Exception)
                {
                    /* error log */
                }
                finally
                {
                    client.Dispose();
                }
                return placeId;

            }
            else
            {
                /* error log */
            }

            client.Dispose();
            return placeId;
        }
    

        // GET: api/Auth
        [HttpGet]
        public async Task<IEnumerable<Domain.Entities.Path>> GetPath([FromQuery] Guid[] productId, DateTime? minTime, DateTime? maxTime)
        {

            Lazy<List<Pickup>[]> lazyPickups = new Lazy<List<Pickup>[]>(() => new List<Pickup>[productId.Length]);
            string[] placeIds = new string[productId.Length];

            for (int i = 0; i < productId.Length; ++i)
            {
                lazyPickups.Value[i] = _context.Pickups
                    .Where(pickup => pickup.ProductId == productId[i])
                    .Where(pickup => (minTime == null || pickup.StartTime >= minTime) && (maxTime == null || pickup.EndTime < maxTime))
                    .ToList();
                placeIds[i] = GetPlaceId(_context.Products.Find(productId[i]).Restaurant.Address);
            }

            string urlParameters = "?";
            
            for(int i = 0; i < lazyPickups.Value.Length; ++i)
            {
                urlParameters += "placeId=" + placeIds[i] + "&visitAmount=" + lazyPickups.Value[i].Count;
                foreach (var pickup in lazyPickups.Value[i])
                {
                    urlParameters += "&pickupId=" + pickup.Id + "&startTime=" + pickup.StartTime.ToString()
                        + "&endTime=" + pickup.EndTime.ToString();
                }
            }

            urlParameters += minTime == null ? "" : minTime.ToString();
            urlParameters += maxTime == null ? "" : maxTime.ToString();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7251/visitplanner/path");

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Domain.Entities.Path> paths = null;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    paths = await response.Content?.ReadFromJsonAsync<IEnumerable<Domain.Entities.Path>>();
                }
                catch (Exception)
                {
                    /* error log */
                }
                finally
                {
                    client.Dispose();
                }
                return paths;

            }
            else
            {
                /* error log */
            }

            client.Dispose();
            return paths;
        }

    }
}
