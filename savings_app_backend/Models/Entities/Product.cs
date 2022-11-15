﻿using Newtonsoft.Json.Converters;
using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Product
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }

        public Guid RestaurantID { get; set; }
        public Restaurant? Restaurant { get; set; }


        public List<Pickup>? Pickups { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; set; }

        public float AmountPerUnit { get; set; }
        public int AmountOfUnits { get; set; }

        public float Price { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public DateTime ShelfLife { get; set; }

        public string? Description { get; set; }
    }
}
