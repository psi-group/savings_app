using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceUserAuth
    {
        public DataAccessServiceUserAuth(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "SeedData\\data" + "\\" +  "users.json"; }
        }

        public IEnumerable<UserAuth> GetUserAuths()
        {
            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAuth[]>(jsonFile);
        }

    }
}