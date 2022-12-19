using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class BuyerPutDTORequest
    {
        public BuyerPutDTORequest(string? name, UserAuthPutDTORequest? userAuth, AddressPutDTORequest? address, IFormFile? image)
        {
            Name = name;
            UserAuth = userAuth;
            Address = address;
            Image = image;
        }

        public BuyerPutDTORequest()
        {

        }

        public string? Name { get; set; }
        public UserAuthPutDTORequest? UserAuth { get; set; }
        public AddressPutDTORequest? Address { get; set; }
        public IFormFile? Image { get; set; }
    }
}
