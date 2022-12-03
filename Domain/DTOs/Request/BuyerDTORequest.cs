using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Domain.DTOs.Request
{
    public class BuyerDTORequest
    {
        public string Name { get; set; }
        public UserAuthDTORequest UserAuth { get; set; }
        public AddressDTORequest? Address { get; set; }
        public IFormFile Image { get; set; }
    }
}
