using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using StackExchange.Redis;
using System.Text.Json;
using System;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
        
        //Our basket is gonna store as string in our redis database. 
        //Then we take our object, json that comes up from the client 
        //and we are going to serialize that into a string which is 
        //stored in our redish database as a string and then we want 
        // to get it out we are going to deserialize it back into 
        //something we can use and will deserialize into customer basket.
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId); 

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }
        
        //replace the existing basket in our redish database and just replace 
        // it with whatever is coming up from the client as the new basket.
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);

        }
    }
}