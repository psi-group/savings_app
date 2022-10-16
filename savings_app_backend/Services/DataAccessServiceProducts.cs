using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
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

        public Product GetById(string id)
        {
            var products = GetProducts();
            return products.SingleOrDefault(p => p.Id == id + "");
        }

        public IEnumerable<Product> GetWithFilters(string[] filters, string searchText)
        {

            SavingsList<Product> _products = new SavingsList<Product>(GetProducts());

            return _products.Search(filters, searchText);
           
        }

        public void AddProduct(Product product)
        {
            
            IEnumerable<Product> products = GetProducts();

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
            
        }
        
        public void DeleteProduct(Product product)
        {
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
        }


    }
}