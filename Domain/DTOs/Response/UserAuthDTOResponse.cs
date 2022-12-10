namespace Domain.DTOs.Response
{
    public class UserAuthDTOResponse
    {
        public UserAuthDTOResponse(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}