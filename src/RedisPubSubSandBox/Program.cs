using System.Threading.Tasks;

namespace RedisPubSubSandBox
{
    using System;
    using StackExchange.Redis;
    class Program
    {
        private const string RedisConnectionString = "127.0.0.1:6379"; // Alternatively you can configure a connection to Redis with Azure.
        private const string MyChannel = "Master";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Redis pub sub!");
            
            using (var connect = await ConnectionMultiplexer.ConnectAsync(RedisConnectionString))
            {
                var subscriber = connect.GetSubscriber();

                // React on a message on channel :
                await subscriber.SubscribeAsync(MyChannel, OnMessageReceived);

                // Pub a message on channel 
                await subscriber.PublishAsync(MyChannel, "My Message !");
            }

            Console.ReadKey();
        }

        // This method will be executed when a message is published on channel "Master" :
        private static void OnMessageReceived(RedisChannel channel, RedisValue receivedValue)
        {
            Console.WriteLine($"Message received : {receivedValue}");
        }
    }
}
