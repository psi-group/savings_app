using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Request
{
    public class RestaurantDTORequest
    {
        public string Name { get; set; }
        public UserAuth UserAuth { get; set; }
        public Address? Address { get; set; }
        public IFormFile Image { get; set; }

        public bool Open { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }
        public string? SiteRef { get; set; }
    }
}
