using savings_app_backend.Models;
using System.Collections;

namespace savings_app_backend.Models
{
    public class Restaurant : SavingsAppObj
    {


        public bool Open { get; set; }

        public string? Image { get; set; }

        public string Description { get; set; }

        public List<Product> ProductsList { get; set; }
       

        public string ShortDescription { get; set; }

        public string SiteRef { get; set; }


    }
}
