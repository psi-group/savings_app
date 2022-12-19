using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class UserAuthPutDTORequest
    {
        public UserAuthPutDTORequest(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public UserAuthPutDTORequest() { }

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
