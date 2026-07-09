using E_Commerce.Domain.Contracts.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase(); 

        public async Task<string?> GetAsync(string Key, CancellationToken ct = default)
        {
           var value = await _database.StringGetAsync(Key);
            if(value.IsNullOrEmpty) return null;
            return value;
        }

        public async Task SetAsync(string Key, object value, TimeSpan? duration = null, CancellationToken ct = default)
        {
            var redisValue = JsonSerializer.Serialize(value);

            var result = await _database.StringSetAsync(Key, redisValue, duration ?? TimeSpan.FromMinutes(1));

        }
    }
}
