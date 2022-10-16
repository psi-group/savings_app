using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace savings_app_backend.Models
{
    public class Product : SavingsAppObj
    {

        public string Id { get; set; }

        public string RestaurantID { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AmountType))]
        public AmountType AmountType { get; set; }

        public float AmountPerUnit { get; set; }
        public int AmountOfUnits { get; set; }

        public float Price { get; set; }

        public string Category { get; set; }

        public string PictureURL { get; set; }

        public string Name { get; set; }

        public DateTime ShelfLife { get; set; }

       public string Description { get; set; }

        public override string ToString()
        {

            StringBuilder json = new StringBuilder();

            json.Append("{\n").Append("\"Id\": ").Append("\"" + Id + "\",\n");
            json.Append("\"RestaurantID\": ").Append("\"" + RestaurantID + "\",\n");
            json.Append("\"PictureURL\": ").Append("\"" + PictureURL + "\",\n");
            json.Append("\"Name\": ").Append("\"" + Name + "\",\n");
            json.Append("\"ShelfLife\": ").Append("\"" + ShelfLife + "\",\n");
            json.Append("\"Description\": ").Append("\"" + Description + "\"\n");

            json.Append("}");
            return json.ToString();
        }
    }
}
