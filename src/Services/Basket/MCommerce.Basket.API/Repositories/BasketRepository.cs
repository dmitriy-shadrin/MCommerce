using MCommerce.Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MCommerce.Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }        

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            var basketAsJSON = JsonSerializer.Serialize<ShoppingCart>(basket);
            await _redisCache.SetStringAsync(basket.UserName, basketAsJSON);

            return await GetBasketAsync(basket.UserName);
        }
    }
}
