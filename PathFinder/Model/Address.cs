using System.Text;

namespace PathFinder.Model
{
    [Serializable]
    public class Address
    {
        public Address(string country, string city, string streeName, int houseNumber)
        {
            Country = country;
            City = city;
            StreetName = streeName;
            HouseNumber = houseNumber;
        }

        public Address()
        {

        }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int AppartmentNumber { get; set; }
        public int PostalCode { get; set; }
    }

}
