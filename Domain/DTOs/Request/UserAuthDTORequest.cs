using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class UserAuthDTORequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
