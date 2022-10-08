using savings_app_backend.Models;
using System.Collections;

namespace savings_app.Models
{
    public class Restaurant : User
    {

        public static int Count = 0;

        public PickupTime[] PickupTimes { get; set; }

        public int PositionCount { set; get; }

        public bool Open { get; set; }

        public string? Image { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public List<Product> ProductsList { get; set; }
       

        public string ShortDescription { get; set; }

        public string SiteRef { get; set; }

        

        public Restaurant() { Count++; }

    }
}
