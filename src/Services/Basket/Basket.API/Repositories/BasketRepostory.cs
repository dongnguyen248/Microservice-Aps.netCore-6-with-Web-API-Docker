using Basket.API.DiscountGrpcServices;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
   
    public class BasketRepostory : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        
        public BasketRepostory(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task DeleteShoppingCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart?> GetShoppingCart(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateShoppingCart(ShoppingCart cart)
        {
            
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetShoppingCart(cart.UserName);
        }
    }
}
