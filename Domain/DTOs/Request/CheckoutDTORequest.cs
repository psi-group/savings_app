using System.ComponentModel.DataAnnotations;


namespace Domain.DTOs.Request
{
    public class CheckoutDTORequest
    {
        public CheckoutDTORequest(List<ProductToBuyDTORequest>? productsToBuy, Guid? buyerId)
        {
            this.productsToBuy = productsToBuy;
            this.buyerId = buyerId;
        }

        public CheckoutDTORequest()
        {

        }

        [Required]
        public List<ProductToBuyDTORequest>? productsToBuy { get; set; }
        [Required]
        public Guid? buyerId { get; set; }
    }
}
