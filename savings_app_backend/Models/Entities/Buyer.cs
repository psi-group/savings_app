using System.Collections;

namespace savings_app_backend.Models.Entities
{
    public class Buyer : User
    {
        public Buyer()
        {

        }
        public Buyer(Guid id, string name, Guid userAuthId, Guid addressId, string imageName)
        {
            Id = id;
            Name = Name;
            UserAuthId = userAuthId;
            AddressId = addressId;
            ImageName = imageName;
        }
    }
}
