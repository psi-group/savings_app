using Domain.Entities.OrderAggregate;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.Response
{
    public class OrderItemDTOResponse
    {
        public OrderItemDTOResponse(Guid id, Guid orderId, Guid productId, Guid pickupId, int unitsOrdered, float price, OrderItemStatus orderItemStatus)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            PickupId = pickupId;
            UnitsOrdered = unitsOrdered;
            Price = price;
            OrderItemStatus = orderItemStatus;
        }

        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public Guid PickupId { get; set; }

        public int UnitsOrdered { get; set; }

        public float Price { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(OrderItemStatus))]
        public OrderItemStatus OrderItemStatus { get; set; }
    }
}
