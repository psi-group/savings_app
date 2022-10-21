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
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" +  "orders.json"; }
        }

        public IEnumerable<Order> GetOrders()
        {

            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Order[]>(jsonFile);
        }

        public IEnumerable<Order> GetByBuyerId(Guid buyerId)
        {
            var orders = GetOrders();
            var filteredOrders = orders.Where(p => p.buyerId.Equals(buyerId));

            return filteredOrders;
        }

        public void CreateOrder(Order order)
        {
            order.Id = Guid.NewGuid();

            IEnumerable<Order> orders = GetOrders();

            orders = orders.Concat(new[] { order });

            var ordersJson = Newtonsoft.Json.JsonConvert.SerializeObject(orders, Formatting.Indented);

            File.WriteAllText(JsonFileName, ordersJson.ToString());
        }


    }
}