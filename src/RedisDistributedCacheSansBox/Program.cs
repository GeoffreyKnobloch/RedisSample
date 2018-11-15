using System;
using System.Threading.Tasks;

namespace RedisDistributedCacheSansBox
{
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Redis distributed cache!");


            var services = new ServiceCollection()
                .AddDistributedRedisCache(options =>
                {
                    options.Configuration = "127.0.0.1";
                    options.InstanceName = "Master";
                }).BuildServiceProvider();
            
            // Redis distributed cache :
            var distributedCache = services.GetService<IDistributedCache>();
            string cacheKey = "myKey";
            var myValue = await distributedCache.GetStringAsync(cacheKey);

            Console.WriteLine($"First call to Redis distributed cache, received value: {myValue}");
            
            if (string.IsNullOrEmpty(myValue))
            {
                await distributedCache.SetStringAsync(cacheKey, "ValueToAdd");
                var myValueAfterInsert = await distributedCache.GetStringAsync(cacheKey);
                Console.WriteLine($"Second call to Redis distributed cache, received value: {myValueAfterInsert}");
            }


            Console.ReadKey();
        }
    }
}
