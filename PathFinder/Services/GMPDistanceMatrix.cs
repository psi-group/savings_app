using PathFinder.Model;
using System.Net.Http.Headers;

namespace PathFinder.Services
{
    public class GMPDistanceMatrix
    {
        public GMPDistanceMatrix(IConfiguration config)
        {
            Configuration = config;
        }

        private readonly IConfiguration Configuration;
        const string URL = "https://maps.googleapis.com/maps/api/distancematrix/json";

        public int? GetDurationDepartAt(string placeId1, string placeId2, DateTime departAt)
        {
            var seconds = (int)(departAt - DateTime.UnixEpoch).TotalSeconds;
            
            string urlParameters = "?departure_time=" + seconds + "&origins=place_id:" + placeId1 + "&destinations=place_id:"
                + placeId2 + "&key=" + Configuration["GoogleAPIKey"];

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            int? duration = null;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    duration = response.Content?.ReadFromJsonAsync<GMPDistanceMatrixResponse>()?.Result?.Rows?[0]?.Elements?[0]?.Duration?.value;
                }
                catch(Exception)
                {
                    /* error log */
                }
                finally
                {
                    client.Dispose();
                }
                return duration;
                
            }
            else
            {
                /* error log */
            }

            client.Dispose();
            return duration;
        }

        public int? GetDurationArriveBy(string placeId1, string placeId2, DateTime arriveBy)
        {

            var seconds = (int)(arriveBy - DateTime.UnixEpoch).TotalSeconds;
            string urlParameters = "?arrival_time=" + seconds + "&origins=place_id:" + placeId1 + "&destinations=place_id:"
                + placeId2 + "&key=" + Configuration["GoogleAPIKey"];

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            int? duration = null;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    duration = response.Content?.ReadFromJsonAsync<GMPDistanceMatrixResponse>()?.Result?.Rows?[0]?.Elements?[0]?.Duration?.value;
                }
                catch
                {
                    /* error log */
                }
                finally
                {
                    client.Dispose();
                }
                return duration;
            }
            else
            {
                /* error log */
            }

            client.Dispose();
            return duration;
        }

    }
}
