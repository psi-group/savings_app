using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class AddressPutDTORequest
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public int? AppartmentNumber { get; set; }
        public int? PostalCode { get; set; }

        public AddressPutDTORequest(string? country, string? city, string? streetName,
            int? houseNumber, int? appartamentNumber, int? postalCode)
        {
            Country = country;
            City = city;
            StreetName = streetName;
            HouseNumber = houseNumber;
            AppartmentNumber = appartamentNumber;
            PostalCode = postalCode;
        }

        public AddressPutDTORequest()
        {

        }
    }
}
