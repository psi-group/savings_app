using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using savings_app_backend.Models;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceProducts
    {
        public DataAccessServiceProducts(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" +  "products.json"; }
        }

        public IEnumerable<Product> GetProducts()
        {

            var jsonFile = File.ReadAllText(JsonFileName);
            
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Product[]>(jsonFile);
        }

        public Product GetById(Guid id)
        {
            var products = GetProducts();
            return products.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetWithFilters(string[] filters, string searchText)
        {

            SearchingList<Product> _products = new SearchingList<Product>(GetProducts());

            var resultsAfterSearch = _products.Search(searchText, (Product prd) => prd.Name);

            
            if(filters.Length == 0)
                return resultsAfterSearch;

            ((List<Product>)resultsAfterSearch).RemoveAll(el => {
                bool delete = false;

                foreach(string filter in filters){
                    delete = el.Category.Equals(filter) ? true : delete;
                }

                return !delete;
            });

            return resultsAfterSearch;

        }

        public void AddProduct(Product product)
        {
            
            IEnumerable<Product> products = GetProducts();

            
            product.Id = Guid.NewGuid();

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
            
        }

        public void Delete(Guid id)
        {
            var products = GetProducts();

            ((List<Product>)products).RemoveAll((product) => product.Id == id);

            var productsJson = Newtonsoft.Json.JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(JsonFileName, productsJson.ToString());

        }


    }
}