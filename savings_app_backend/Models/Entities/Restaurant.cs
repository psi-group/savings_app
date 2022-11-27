using System.Collections;

namespace savings_app_backend.Models.Entities
{
    public class Restaurant : User
    {

        public Restaurant()
        {

        }

        public Restaurant(Guid id, string name, Guid userAuthId, Guid addressId, string imageName, 
            bool open, double rating, string description, string shortDescription, string siteRef)
        {
            Name = name;
            UserAuthId = userAuthId;
            AddressId = addressId;
            ImageName = imageName;
            Id = id;
            Open = open;
            Rating = rating;
            Description = description;
            ShortDescription = shortDescription;
            SiteRef = siteRef;
        }

        public bool Open { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public List<Product>? Products { get; set; }
        public string ShortDescription { get; set; }

        public string SiteRef { get; set; }

        public static implicit operator Restaurant(Task<Restaurant?> v)
        {
            throw new NotImplementedException();
        }
    }
}
