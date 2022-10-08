using savings_app_backend.Models;

namespace savings_app.Models
{
    public class User
    {
        
        public string Id { get; set; } 

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool IsRestaurant { get; set; } = false;

        //public string _userName { get; set; }
       
        //string _password;

        // need to extend this class with 'Seller' and 'Buyer'

        public void AddNewListing(Product product){

        }

        public void ChangePassword(string newPassword){
            this.Password = newPassword;
        }

        /*
        public void ChangeUsername(string newUsername){
            this._userName= newUsername;
        }
        */
    }
}
