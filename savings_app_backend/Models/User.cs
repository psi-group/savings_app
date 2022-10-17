using savings_app_backend.Models;
using System.Text;

namespace savings_app_backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string ToString()
        {
            StringBuilder json = new StringBuilder();

            json.Append("{\n").Append("\"Id\": ").Append("\"" + Id + "\",\n");
            json.Append("\"Name\": ").Append("\"" + Name + "\",\n");
            json.Append("\"Password\": ").Append("\"" + Password + "\",\n");
            json.Append("\"Email\": ").Append("\"" + Email + "\"\n");

            json.Append("}");
            return json.ToString();
        }
    }
}
