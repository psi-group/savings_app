using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class UserLoginDTO
    {

        public UserLoginDTO(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public UserLoginDTO() { }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}