using savings_app_backend.Models;
using System.Collections;

namespace savings_app_backend.Models
{
    public class Category
    {
        public string CategoryName { get; set; }
        public string[] Subcategories { get; set; }
    }
}
