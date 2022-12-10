﻿using Domain.Entities.OrderAggregate;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Response
{
    public class OrderDTOResponse
    {
        public OrderDTOResponse(Guid id, Guid buyerId, List<OrderItemDTOResponse> orderItems)
        {
            Id = id;
            BuyerId = buyerId;
            OrderItems = orderItems;
        }

        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public List<OrderItemDTOResponse> OrderItems { get; set; }
    }
}
