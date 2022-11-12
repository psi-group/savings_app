using System.Text;

namespace savings_app_backend.Models.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int AppartmentNumber { get; set; }
        public int PostalCode { get; set; }
    }
}
