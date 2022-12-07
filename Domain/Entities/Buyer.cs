using Domain.Entities.OrderAggregate;

namespace Domain.Entities
{
    public class Buyer : User
    {
        public Address? Address { get; private set; }
        public List<Order>? Orders { get; private set; }

        public Buyer()
        {

        }
        public Buyer(Guid id, string name, UserAuth userAuth, Address? address, string imageName)
        {
            Id = id;
            Name = name;
            UserAuth = userAuth;
            Address = address;
            ImageName = imageName;
        }
    }
}
