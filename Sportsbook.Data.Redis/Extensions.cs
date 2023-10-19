using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;

namespace Sportsbook.Data.Redis
{
    public static class Extensions
    {
        public static IServiceCollection AddRedisCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConfig>(configuration.GetSection(nameof(RedisConfig)));
            
            services.AddStackExchangeRedisCache(options =>
            {
                var config = configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>();

                options.Configuration = config.ConnectionString;
                options.InstanceName = "Sportsbook";
                
            });
            return services;
        }

        public static async Task<T> GetThenSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> func, DistributedCacheEntryOptions cacheEntryOptions)
        {
            var data = await cache.GetAsync(key);

            if (data == null)
            {
                data = (await func()).ObjectToByteArray();
                await cache.SetAsync(key, data, cacheEntryOptions);
            }

            return data.ByteArrayToObject<T>();
        }

        public static T ByteArrayToObject<T>(this byte[] bytes)
        {
            if (bytes == null)
            {
                return default(T);
            }
            return (T)JsonSerializer.Deserialize(Encoding.UTF8.GetString(bytes), typeof(T));
        }

        public static byte[] ObjectToByteArray<T>(this T obj)
        {
            var str = JsonSerializer.Serialize(obj);
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
