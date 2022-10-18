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

            var numQuery =
            from product in products
            where product.Id == id
            select product;

            return numQuery.ToArray()[0];
        }

        public IEnumerable<Product> GetWithFilters(string[] filters, string searchText, string order = "by_id")
        {

            SearchingList<Product> _products = new SearchingList<Product>(GetProducts());

             

            var resultsAfterSearch = _products.Search(searchText, (Product prd) => prd.Name);

            var products = ((List<Product>)resultsAfterSearch);


            products.Sort(delegate (Product x, Product y)
            {
                if (order == "by_id")
                {
                    if (x.Id == null && y.Id == null) return 0;
                    else if (x.Id == null) return -1;
                    else if (y.Id == null) return 1;
                    else return x.Id.CompareTo(y.Id);
                }
                else if (order == "by_name")
                {
                    if (x.Name == null && y.Name == null) return 0;
                    else if (x.Name == null) return -1;
                    else if (y.Name == null) return 1;
                    else return x.Name.CompareTo(y.Name);
                }
                else
                {
                    if (x.Price == null && y.Price == null) return 0;
                    else if (x.Price == null) return -1;
                    else if (y.Price == null) return 1;
                    else return x.Price.CompareTo(y.Price);
                }

            });

            if (filters.Length == 0)
                return products;
            

            ((List<Product>)resultsAfterSearch).RemoveAll(el => {
                bool delete = false;

                foreach(string filter in filters){
                    delete = el.Category.Equals(filter) ? true : delete;
                }

                return !delete;
            });

            return (IEnumerable<Product>)products;

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

        public void UpdateProduct(Product productToUpdate)
        {
            var products = GetProducts();

            ((List<Product>)products).RemoveAll((product) => product.Id == productToUpdate.Id);
            ((List<Product>)products).Add(productToUpdate);

            var productsJson = Newtonsoft.Json.JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText(JsonFileName, products.ToString());
        }


    }
}