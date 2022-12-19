using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class BuyerDTORequest
    {
        public BuyerDTORequest(string? name, UserAuthDTORequest? userAuth, AddressDTORequest? address, IFormFile? image)
        {
            Name = name;
            UserAuth = userAuth;
            Address = address;
            Image = image;
        }

        public BuyerDTORequest()
        {

        }

        [Required]
        public string? Name { get; set; }
        [Required]
        public UserAuthDTORequest? UserAuth { get; set; }
        public AddressDTORequest? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
