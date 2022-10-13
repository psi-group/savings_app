using System;
using System.ComponentModel.DataAnnotations;

namespace savings_app_backend.Models{

    public class SavingsAppObj{

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; protected set; }
        public string Email { get; set; }
        public double Rating { get; set; }

        public void ChangePassword(string newPassword)
        {
            this.Password = newPassword;
        }

    }
}