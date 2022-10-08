using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using savings_app_backend.Models;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessService
    {
        public DataAccessService(IWebHostEnvironment webHostEnvironment)
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

            var jsonFileReader = File.OpenText(JsonFileName);

            var products = JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd());
            jsonFileReader.Close();
            return products;
        }

        public Product GetById(int id)
        {
            var products = GetProducts();
            foreach(Product product in products)
            {
                if(product.Id.Equals(id+""))
                {
                    return product;
                }
            }
            return null;
        }

        public IEnumerable<Product> GetBySearchText(string searchText)
        {
            /* more searching logic to implement (use regex here)*/

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
            return filteredProducts;
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
            
            /*var jsonFileWriter = File.OpenWrite(JsonFileName);

            jsonFileWriter.WriteAsync(json.ToString());
            jsonFileWriter.Close();*/
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