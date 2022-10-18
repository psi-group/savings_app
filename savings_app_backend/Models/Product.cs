using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models
{
    public class Product
    {

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }
        public Guid RestaurantID { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; set; }

        public float AmountPerUnit { get; set; }
        public int AmountOfUnits { get; set; }

        public float Price { get; set; }

        public string PictureURL { get; set; }

        public DateTime ShelfLife { get; set; }

       public string Description { get; set; }
    }
}
