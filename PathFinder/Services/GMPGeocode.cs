using System.Net.Http.Headers;
using System.Net;
using PathFinder.Model;

namespace PathFinder.Services
{
    public class GMPGeocode
    {
        public GMPGeocode(IConfiguration config)
        {
            Configuration = config;
        }

        private readonly IConfiguration Configuration;

        const string URL = "https://maps.googleapis.com/maps/api/geocode/json";

        public string? GetPlaceId(Address Address)
        {
            string urlParameters = "?address=" + Address.HouseNumber + "%20" + Address.StreetName + "%20"
                + Address.City + "%20" + Address.Country + "&key=" + Configuration["GoogleAPIKey"];

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string? result = null;
                try
                {
                    result = response.Content?.ReadFromJsonAsync<GMPGeocodeResponse>()?.Result?.Results?[0].Place_Id;
                }
                catch(Exception)
                {
                    /* log error */
                }
                finally
                {
                    client.Dispose();
                }
                return result;
            }
            else
            {
                /* log error */
            }

            client.Dispose();
            return null;
        }
    }
}
