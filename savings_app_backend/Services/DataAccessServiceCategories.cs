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
    public class DataAccessServiceCategories
    {
        public DataAccessServiceCategories(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "data" + "\\" + "categories.json"; }
        }

        public IEnumerable<Category> GetCategories()
        {

            var jsonFileReader = File.OpenText(JsonFileName);

            var categories = JsonSerializer.Deserialize<Category[]>(jsonFileReader.ReadToEnd());
            jsonFileReader.Close();
            return categories;
        }


    }
}