using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ExploreCalifornia.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // This code is only for a demo, its place is not here!!
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var exchangeName = "webappExchange";

                    // channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true); Fanout type exchange(Broadcast to all queues) !!
                    // channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true); Direct type exchange(Sent to all queues that match the given route key)
                    // channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true); Topic type exchange(Sent to all queues that match the given route key or specified pattern)
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Headers, true); // Headers type exchange(Sent to all queues that match all or any headers depend on the additional header named "x-match" with the value "all" or "any")

                    channel.Close();
                }

                connection.Close();
            }


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
