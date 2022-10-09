using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace savings_app_backend.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Restaurant ID is required")]
        public string RestaurantID { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "Product picture is required")]
        public string PictureURL { get; set; }


        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product shelf life is required")]
        public string ShelfLife { get; set; }

        //public int sellerId { get; set; } // User will later be changed to Seller type

        //public double price { get; set; } 

        // need to make enum 'AmountType' to show how amount is represented (unit, volume, mass)
        //public double amount { get; set; }

        //[Required]
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
