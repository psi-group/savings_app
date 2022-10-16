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
    public class DataAccessServicePickups
    {
        public DataAccessServicePickups(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" + "pickups.json"; }
        }

        public IEnumerable<Pickup> GetPickups()
        {

            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Pickup[]>(jsonFile);
        }

    }


}