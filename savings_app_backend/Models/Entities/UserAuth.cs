using System.Text;

namespace savings_app_backend.Models.Entities
{
    public class UserAuth
    {

        public UserAuth(Guid id, string password, string email)
        {
            Id = id;
            Password = password;
            Email = email;
        }
        public UserAuth()
        {

        }
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
