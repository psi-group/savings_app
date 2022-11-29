using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class ProductToBuyDTORequest
    {
        public Guid ProductId { get; private set; }

        public int Amount { get; private set; }
    }
}
