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
    public class DataAccessServicePickups
    {
        public DataAccessServicePickups(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "SeedData\\data" + "\\" + "pickups.json"; }
        }

        public IEnumerable<Pickup> GetPickups()
        {

            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Pickup[]>(jsonFile);
        }

        public void UpdatePickup(Pickup pickupToUpdate)
        {
            var pickups = GetPickups();

            ((List<Pickup>)pickups).RemoveAll((pickup) => pickup.Id == pickupToUpdate.Id);
            ((List<Pickup>)pickups).Add(pickupToUpdate);

            var pickupsJson = Newtonsoft.Json.JsonConvert.SerializeObject(pickups, Formatting.Indented);

            File.WriteAllText(JsonFileName, pickupsJson.ToString());
        }
    }
}