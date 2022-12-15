using Domain.Entities;
using Domain.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.DTOs.Response
{
    public class RestaurantDTOResponse
    {
        public RestaurantDTOResponse(Guid id, string name, AddressDTOResponse address, string? imageUrl,
            bool open, string? description, string? shortDescription, string? siteRef)
        {
            Id = id;
            Name = name;
            Address = address;
            ImageUrl = imageUrl;
            Open = open;
            Description = description;
            ShortDescription = shortDescription;
            SiteRef = siteRef;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public AddressDTOResponse Address { get; set; }
        public string? ImageUrl { get; set; }
        public bool Open { get; set; }
        public double Rating { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? SiteRef { get; set; }
    }
}
