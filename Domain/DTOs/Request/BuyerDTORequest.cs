using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class BuyerDTORequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public UserAuthDTORequest? UserAuth { get; set; }
        public AddressDTORequest? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
