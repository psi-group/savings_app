using Domain.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Response
{
    public class OrderDTOResponse
    {
        public OrderDTOResponse(Guid id, DateTime orderDate, Guid buyerId, List<OrderItemDTOResponse> orderItems)
        {
            Id = id;
            OrderDate = orderDate;
            BuyerId = buyerId;
            OrderItems = orderItems;
        }

        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid BuyerId { get; set; }
        public List<OrderItemDTOResponse> OrderItems { get; set; }
    }
}
