using System.Text;

namespace savings_app_backend.Models.Entities
{
    public class UserAuth
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
