using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Domain.DTOs.Request
{
    public class BuyerDTORequest
    {
        public string Name { get; set; }
        public UserAuth UserAuth { get; set; }
        public Address? Address { get; set; }
        public IFormFile Image { get; set; }

    }
}
