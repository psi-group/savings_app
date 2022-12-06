using Domain.Entities.OrderAggregate;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class OrderDTORequest
    {
        public Guid BuyerId { get; set; }
    }
}
