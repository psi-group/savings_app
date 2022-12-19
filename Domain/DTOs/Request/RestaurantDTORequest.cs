using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class RestaurantDTORequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public UserAuthDTORequest? UserAuth { get; set; }
        [Required]
        public AddressDTORequest? Address { get; set; }

        public IFormFile? Image { get; set; }

        public bool? Open { get; set; } = true;

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }
        public string? SiteRef { get; set; }


        public RestaurantDTORequest(string? name, UserAuthDTORequest? userAuth, AddressDTORequest? address, IFormFile? image, bool? open, string? description, string? shortDescription, string? siteRef)
        {
            Name = name;
            UserAuth = userAuth;
            Address = address;
            Image = image;
            Open = open;
            Description = description;
            ShortDescription = shortDescription;
            SiteRef = siteRef;
        }

        public RestaurantDTORequest() { }
    }
}
