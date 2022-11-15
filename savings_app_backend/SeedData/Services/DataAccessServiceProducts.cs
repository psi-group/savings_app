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
using savings_app_backend.Models.Entities;

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
            get { return WebHostEnvironment.ContentRootPath + "\\" + "SeedData\\data" + "\\" +  "products.json"; }
        }

        public IEnumerable<Product> GetProducts()
        {

            var jsonFile = File.ReadAllText(JsonFileName);
            
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Product[]>(jsonFile);
        }
    }
}