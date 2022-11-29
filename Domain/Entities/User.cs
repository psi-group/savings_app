using Domain.Entities.OrderAggregate;
using Domain.Interfaces.Entities;

namespace Domain.Entities
{
    public abstract class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; protected set; }

        public List<Order>? Orders { get; protected set; }

        public UserAuth UserAuth { get; protected set; }

        public Address? Address { get; protected set; }

        public string ImageName { get; protected set; }

    }
}
