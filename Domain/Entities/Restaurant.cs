namespace Domain.Entities
{
    public class Restaurant : User
    {

        private Restaurant()
        {

        }

        public Restaurant(Guid id, string name, UserAuth userAuth, Address address, string? imageUrl, 
            bool open, string? description, string? shortDescription, string? siteRef)
        {
            Id = id;
            Name = name;
            UserAuth = userAuth;
            Address = address;
            ImageUrl = imageUrl;
            Open = open;
            Description = description;
            ShortDescription = shortDescription;
            SiteRef = siteRef;
        }

        public bool Open { get; private set; }

        public Address Address { get; private set; }
        public double Rating { get; private set; }
        public string? Description { get; private set; }
        public List<Product>? Products { get; private set; }
        public string? ShortDescription { get; private set; }
        public string? SiteRef { get; private set; }
    }
}
