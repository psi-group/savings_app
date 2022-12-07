using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Response
{
    public class ProductDTOResponse
    {
        public ProductDTOResponse(Guid id, string name, bool isHidden, Category category,
            Guid restaurantId, AmountType amountType, float amountPerUnit, int amountOfUnits,
            float price, string imageName, DateTime shelfLife, string? description)
        {
            Id = id;
            Name = name;
            IsHidden = isHidden;
            Category = category;
            RestaurantID = restaurantId;
            AmountType = amountType;
            AmountPerUnit = amountPerUnit;
            AmountOfUnits = amountOfUnits;
            Price = price;
            ImageName = imageName;
            ShelfLife = shelfLife;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        public Guid RestaurantID { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; set; }

        public float AmountPerUnit { get; set; }

        public int AmountOfUnits { get; set; }

        public float Price { get; set; }

        public string ImageName { get; set; }

        public DateTime ShelfLife { get; set; }

        public string? Description { get; set; }
    }
}
