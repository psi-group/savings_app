﻿using savings_app_backend.Models;
using Newtonsoft.Json;
using savings_app_backend.Models.Entities;

namespace savings_app_backend.WebSite.Services
{
    public class DataAccessServiceRestaurants
    {
        public DataAccessServiceRestaurants(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return WebHostEnvironment.ContentRootPath + "\\" + "SeedData\\data" + "\\" +  "restaurants.json"; }
        }

       
        public IEnumerable<Restaurant> GetRestaurants()
        {
            var jsonFile = File.ReadAllText(JsonFileName);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Restaurant[]>(jsonFile);
        }

    }
}