using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class ProductToBuyDTORequest
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public int Amount { get; set; }
    }
}
