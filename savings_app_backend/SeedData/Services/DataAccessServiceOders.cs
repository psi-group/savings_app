using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceOrders
    {
        public DataAccessServiceOrders(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "SeedData\\data" + "\\" +  "orders.json"; }
        }

        public IEnumerable<Order> GetOrders()
        {

            var jsonFile = File.ReadAllText(JsonFileName);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Order[]>(jsonFile);

            return result;
        }
    }
}