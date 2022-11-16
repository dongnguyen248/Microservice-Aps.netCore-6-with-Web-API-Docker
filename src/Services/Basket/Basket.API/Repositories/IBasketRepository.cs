using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string userName);
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart);
        Task DeleteShoppingCart(string userName);
    }
}
