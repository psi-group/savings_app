using Domain.Entities.OrderAggregate;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Request
{
    public class OrderDTORequest
    {
        [Required]
        public Guid? BuyerId { get; set; }
        [Required]
        public List<OrderItem>? OrderItems { get; set;}
    }
}
