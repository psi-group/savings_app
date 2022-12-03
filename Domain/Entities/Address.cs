namespace Domain.Entities
{
    public class Address
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public int HouseNumber { get; private set; }
        public int AppartmentNumber { get; private set; }
        public int PostalCode { get; private set; }

        public Address (string country, string city, string streetName,
            int houseNumber, int appartamentNumber, int postalCode)
        {
            Country = country;
            City = city;
            StreetName = streetName;
            HouseNumber = houseNumber;
            AppartmentNumber = appartamentNumber;
            PostalCode = postalCode;
        }

        public Address()
        {

        }
    }
}
