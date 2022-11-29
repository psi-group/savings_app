using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public EventHandler<ProductSoldOutEventArgs>? ProductSoldOut;
        public EventHandler<ProductSoldEventArgs>? ProductSold;

        public string Name { get; private set; }
        public bool IsHidden { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Category))]
        public Category Category { get; private set; }

        public Guid RestaurantID { get;  private set; }
        public Restaurant? Restaurant { get; private set; }

        public List<Pickup>? Pickups { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; private set; }

        public float AmountPerUnit { get; private set; }
        public int AmountOfUnits { get; private set; }

        public float Price { get; private set; }

        public string ImageName { get; private set; }

        public DateTime ShelfLife { get; private set; }

        public string? Description { get; private set; }

        public Product(Guid id, string name, Category category, Guid restaurantId, AmountType amountType,
            float amountPerUnit, int amountOfUnits, float price, string imageName, DateTime shelfLife,
            string? description)
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

        public void BuyAmount(int amount, string buyerEmail)
        {
            if(amount > AmountOfUnits)
            {
                throw new NotEnoughProductAmountException();
            }
            else
            {
                AmountOfUnits -= amount;
                if(ProductSold != null)
                    ProductSold(this, new ProductSoldEventArgs(amount, buyerEmail));
                    

                if(AmountOfUnits == 0)
                {
                    if (ProductSoldOut != null)
                        ProductSoldOut(this, new ProductSoldOutEventArgs());
                }
            }
        }
    }
}
