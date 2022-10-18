using savings_app_backend.Models;
using Newtonsoft.Json;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceRestaurants
    {
        public DataAccessServiceRestaurants(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" +  "restaurants.json"; }
        }

       
        public IEnumerable<Restaurant> GetRestaurants()
        {
            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Restaurant[]>(jsonFile);
        }

        public IEnumerable<Restaurant> GetWithSearch(string searchText)
        {
            SearchingList<Restaurant> restaurants = new SearchingList<Restaurant>(GetRestaurants());
           
            return  restaurants.Search(searchText, (Restaurant res) => res.Name);
        }

        public Restaurant GetById(Guid id)
        {
            var restaurants = GetRestaurants();

            return restaurants.SingleOrDefault(r => r.Id == id);
        }

    }
}