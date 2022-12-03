using Domain.DTOs.Request;

namespace Application.Services.Interfaces
{
    public interface IShopService
    {
        public Task Checkout(List<ProductToBuyDTORequest> productsToBuy);
    }
}
