using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class AddressDTORequest
    {
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public int? HouseNumber { get; set; }
        public int? AppartmentNumber { get; set; }
        [Required]
        public int? PostalCode { get; set; }

        public AddressDTORequest(string? country, string? city, string? streetName,
            int? houseNumber, int? appartamentNumber, int? postalCode)
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
