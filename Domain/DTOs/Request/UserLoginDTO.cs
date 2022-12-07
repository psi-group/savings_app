using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class UserLoginDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}