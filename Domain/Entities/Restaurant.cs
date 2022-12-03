namespace Domain.Entities
{
    public class Restaurant : User
    {

        private Restaurant()
        {

        }

        public Restaurant(Guid id, string name, UserAuth userAuth, Address? address, string imageName, 
            bool open, string? description, string? shortDescription, string? siteRef)
        {
            Id = id;
            Name = name;
            UserAuth = userAuth;
            Address = address;
            ImageName = imageName;
            Open = open;
            Description = description;
            ShortDescription = shortDescription;
            SiteRef = siteRef;
        }

        public bool Open { get; private set; }
        public double Rating { get; private set; }
        public string? Description { get; private set; }
        public List<Product>? Products { get; private set; }
        public string? ShortDescription { get; private set; }
        public string? SiteRef { get; private set; }
    }
}
