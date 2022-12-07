using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class CheckoutDTORequest
    {
        [Required]
        public List<ProductToBuyDTORequest>? productsToBuy { get; set; }
        [Required]
        public Guid? buyerId { get; set; }
    }
}
