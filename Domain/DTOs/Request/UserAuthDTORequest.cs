using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class UserAuthDTORequest
    {
        public UserAuthDTORequest(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public UserAuthDTORequest() { }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
