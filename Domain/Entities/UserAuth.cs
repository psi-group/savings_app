namespace Domain.Entities
{
    public class UserAuth
    {
        public string Password { get; private set; }
        public string Email { get; private set; }

        public UserAuth(string password, string email)
        {
            Password = password;
            Email = email;
        }
        public UserAuth()
        {

        }
    }
}
