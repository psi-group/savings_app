using System.Collections;

namespace savings_app_backend.Models.Entities
{
    public class Restaurant : User
    {
        public bool Open { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public UserAuth UserAuth { get; set; }

        public string ShortDescription { get; set; }

        public string SiteRef { get; set; }
    }
}
