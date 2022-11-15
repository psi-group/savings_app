using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using savings_app_backend.Extention;
using savings_app_backend.Models;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathFinderController : ControllerBase
    {
        private readonly savingsAppContext _context;

        public PathFinderController(savingsAppContext context)
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
        public async Task<IEnumerable<Models.Entities.Path>> GetPath([FromQuery] Guid[] productId, DateTime? minTime, DateTime? maxTime)
        {

            List<Pickup>[] pickups = new List<Pickup>[productId.Length];
            string[] placeIds = new string[productId.Length];

            for (int i = 0; i < productId.Length; ++i)
            {
                pickups[i] = _context.Pickups
                    .Where(pickup => pickup.ProductId == productId[i])
                    .Where(pickup => (minTime == null || pickup.startTime >= minTime) && (maxTime == null || pickup.endTime < maxTime))
                    .ToList();
                placeIds[i] = GetPlaceId(_context.Products.Find(productId[i]).Restaurant.Address);
            }

            string urlParameters = "?";
            
            for(int i = 0; i < pickups.Length; ++i)
            {
                urlParameters += "placeId=" + placeIds[i] + "&visitAmount=" + pickups[i].Count;
                foreach (var pickup in pickups[i])
                {
                    urlParameters += "&pickupId=" + pickup.Id + "&startTime=" + pickup.startTime.ToString()
                        + "&endTime=" + pickup.endTime.ToString();
                }
            }

            urlParameters += minTime == null ? "" : minTime.ToString();
            urlParameters += maxTime == null ? "" : maxTime.ToString();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7251/visitplanner/path");

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Models.Entities.Path> paths = null;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    paths = await response.Content?.ReadFromJsonAsync<IEnumerable<Models.Entities.Path>>();
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
