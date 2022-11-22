using Newtonsoft.Json.Converters;
using savings_app_backend.Events;
using savings_app_backend.Exceptions;
using savings_app_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models.Entities
{
    public class Product : IDable
    {
        public Product(Guid id, string name, Category category, Guid restaurantId, AmountType amountType,
            float amountPerUnit, int amountOfUnits, float price, string imageName, DateTime shelfLife,
            string description)
        {
            Id = id;
            Name = name;
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

        public Product()
        {

        }

        public delegate void ProductSoldOutEventHandler(Product product, string sellerEmail);
        public event ProductSoldOutEventHandler ProductSoldOut;

        public EventHandler<ProductSoldEventArgs> ProductSold;
        public Guid Id { get; set; }

        public string Name { get; set; }

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

        public string Description { get; set; }

        public void ReduceAmount(int amount)
        {
            if(amount > AmountOfUnits)
            {
                throw new NotEnoughProductAmountException();
            }
            else
            {
                AmountOfUnits -= amount;
                if(ProductSold != null)
                    ProductSold(this, new ProductSoldEventArgs(amount, Restaurant.UserAuth.Email));

                if(AmountOfUnits == 0)
                {
                    ProductSoldOut(this, Restaurant.UserAuth.Email);
                }
            }
        }
    }
}
