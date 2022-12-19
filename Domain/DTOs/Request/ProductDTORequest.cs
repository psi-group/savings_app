using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Request
{
    public class ProductDTORequest
    {
        [Required]
        public string? Name { get; set; }
        public bool? IsHidden { get; set; } = false;

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Category))]
        public Category? Category { get; set; }

        [Required]
        public Guid? RestaurantID { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType? AmountType { get; set; }

        [Required]
        public float? AmountPerUnit { get; set; }
        [Required]
        public int? AmountOfUnits { get; set; }
        [Required]
        public float? Price { get; set; }
        [Required]
        public DateTime? ShelfLife { get; set; }
        
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }


        public ProductDTORequest(string? name, bool? isHidden, Category? category, Guid? restaurantID, AmountType? amountType, float? amountPerUnit, int? amountOfUnits, float? price, DateTime? shelfLife, string? description, IFormFile? image)
        {
            Name = name;
            IsHidden = isHidden;
            Category = category;
            RestaurantID = restaurantID;
            AmountType = amountType;
            AmountPerUnit = amountPerUnit;
            AmountOfUnits = amountOfUnits;
            Price = price;
            ShelfLife = shelfLife;
            Description = description;
            Image = image;
        }

        public ProductDTORequest() { }
    }
}
