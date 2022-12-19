using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Request
{
    public class ProductToBuyDTORequest
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public Guid? PickupId { get; set; }
        [Required]
        public int? Amount { get; set; }


        public ProductToBuyDTORequest(Guid? id, Guid? pickupId, int? amount)
        {
            Id = id;
            PickupId = pickupId;
            Amount = amount;
        }


        public ProductToBuyDTORequest() { }
    }
}
