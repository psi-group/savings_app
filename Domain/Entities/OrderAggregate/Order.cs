using Domain.Interfaces.Entities;

namespace Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public Order()
        {

        }

        public Order(Guid id, Guid buyerId)
        {
            Id = id;
            BuyerId = buyerId;
        }

        public DateTime OrderDate { get; private set; } = DateTime.Now;

        public Guid BuyerId { get; private set; }
        public Buyer? Buyer { get; private set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    }
}
