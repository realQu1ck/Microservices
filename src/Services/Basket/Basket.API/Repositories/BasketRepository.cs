using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;
        public BasketRepository(IDistributedCache _redisCache)
        {
            redisCache = _redisCache ?? throw new ArgumentNullException(nameof(BasketRepository));
        }
        public async Task DeleteBasketAsync(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var basket = await redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart model)
        {
            await redisCache.SetStringAsync(model.UserName,
                JsonConvert.SerializeObject(model));
            return await GetBasketAsync(model.UserName);
        }
    }
}
