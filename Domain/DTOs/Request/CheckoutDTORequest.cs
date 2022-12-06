using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class CheckoutDTORequest
    {
        public List<ProductToBuyDTORequest> productsToBuy { get; set; }
        public Guid buyerId { get; set; }
    }
}
