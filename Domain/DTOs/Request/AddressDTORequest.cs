namespace Domain.DTOs.Request
{
    public class AddressDTORequest
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int AppartmentNumber { get; set; }
        public int PostalCode { get; set; }

        public AddressDTORequest(string country, string city, string streetName,
            int houseNumber, int appartamentNumber, int postalCode)
        {
            Country = country;
            City = city;
            StreetName = streetName;
            HouseNumber = houseNumber;
            AppartmentNumber = appartamentNumber;
            PostalCode = postalCode;
        }

        public AddressDTORequest()
        {

        }
    }
}
