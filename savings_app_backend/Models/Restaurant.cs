using savings_app_backend.Models;

namespace savings_app_backend.Models
{
    public class Restaurant : SavingsAppObj
    {


        public PickupTime[] PickupTimes { get; set; }


        public bool Open { get; set; }

        public string? Image { get; set; }


        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string SiteRef { get; set; }


    }
}
