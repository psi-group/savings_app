using savings_app_backend.Models;
using Newtonsoft.Json;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceRestaurants
    {
        DataAccessServiceProducts _dataAccessServiceProducts;
        public DataAccessServiceRestaurants(IWebHostEnvironment webHostEnvironment, DataAccessServiceProducts dataAccessServiceProducts)
        {
            WebHostEnvironment = webHostEnvironment;
            _dataAccessServiceProducts = dataAccessServiceProducts;
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
            
            SavingsList<Restaurant> restaurants = new SavingsList<Restaurant>(GetRestaurants());
           
            return restaurants.Search(filters, searchText);

            
        }

        public Restaurant GetById(string id)
        {
            var restaurants = GetRestaurants();

            return restaurants.SingleOrDefault(r => r.Id == id);
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