namespace Domain.Entities
{
    public class Buyer : User
    {
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
