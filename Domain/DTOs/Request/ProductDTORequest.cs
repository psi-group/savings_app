using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Request
{
    public class ProductDTORequest
    {
        public string Name { get; private set; }
        public bool IsHidden { get; private set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        //[EnumDataType(typeof(Category))]
        public Category Category { get; private set; }

        public Guid RestaurantID { get; private set; }

        public List<Pickup>? Pickups { get; private set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        //[EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; private set; }

        public float AmountPerUnit { get; private set; }
        public int AmountOfUnits { get; private set; }

        public float Price { get; private set; }

        public DateTime ShelfLife { get; private set; }

        public string? Description { get; private set; }

        public IFormFile Image { get; private set; }
    }
}
