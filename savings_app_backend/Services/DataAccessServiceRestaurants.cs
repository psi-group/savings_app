using savings_app.Models;
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

        public IEnumerable<Restaurant> GetWithFilters(string[] filters, string searchText)
        {
            /* more searching logic to implement (use regex here)*/

            string search = null;
            if (searchText != null)
                search = searchText.ToLower();



            var restaurants = GetRestaurants();

            //return restaurants;

            List<Restaurant> filteredRestaurantsBySearch = new List<Restaurant>();
            foreach (Restaurant restaurant in restaurants)
            {
                if (search == null || search == "" || restaurant.Name.ToLower().Contains(search))
                {
                    filteredRestaurantsBySearch.Add(restaurant);
                }
            }

            List<Restaurant> filteredRestaurantsByCategories = new List<Restaurant>();

            if (filters.Length == 0)
                return filteredRestaurantsBySearch;

            int count = 0;
            foreach (string filter in filters)
            {
                if (filter != null)
                    count++;

            }

            if (count == 0)
                return filteredRestaurantsBySearch;

            foreach (Restaurant restaurant in filteredRestaurantsBySearch)
            {
                foreach (string filter in filters)
                {
                    //if (restaurant.Category.Equals(filter)) // no categories of restaurants implemented yet
                    {
                        filteredRestaurantsByCategories.Add(restaurant);
                    }
                }

            }

            return filteredRestaurantsByCategories;
        }

        public Restaurant GetById(int id)
        {
            var restaurants = GetRestaurants();

            return restaurants.SingleOrDefault(r => r.Id == id + "");
        }

        public IEnumerable<Product> GetBySearchText(string searchText)
        {
            /* more searching logic to implement (use regex here)*/
            /*
                        var products = GetProducts();

                        if (searchText.Equals(""))
                        {
                            Console.Write("tuscias");
                            return products;
                        }


                        List<Product> filteredProducts = new List<Product>();   
                        foreach (Product product in products)
                        {
                            if (product.Name.Contains(searchText))
                            {
                                filteredProducts.Add(product);
                            }
                        }
                        return filteredProducts;*/

            return null;
        }

        public void AddProduct(Product product)
        {
            
            /*IEnumerable<Product> products = GetProducts();

            int lastID = int.Parse(products.ToList().Last().Id);
            product.Id = Guid.NewGuid().ToString();

            products = products.Concat(new[] { product });
            
            StringBuilder json = new StringBuilder();

            json = json.Append("[\n");

            foreach(Product prod in products)
            {
                //json.Append("\n\n\n\n" + products.Count() + "\n\n\n\n\n");
                json.Append(prod.ToString());
                json.Append(",");
            }

            json.Remove(json.Length - 1, 1);
            json.Append("\n]");

            File.WriteAllText(JsonFileName, json.ToString());
            
            /*var jsonFileWriter = File.OpenWrite(JsonFileName);

            jsonFileWriter.WriteAsync(json.ToString());
            jsonFileWriter.Close();*/
        }
        
        public void DeleteProduct(Product product)
        {
            /*
            List<Product> products = GetProducts().ToList();

            products.RemoveAll(p => p.Id == product.Id);



            StringBuilder json = new StringBuilder();

            json = json.Append("[\n");

            foreach (Product prod in products)
            {
                json.Append(prod.ToString());
                json.Append(",");
            }

            json.Remove(json.Length - 1, 1);
            json.Append("\n]");

            File.WriteAllText(JsonFileName, json.ToString());
            */
        }


    }
}