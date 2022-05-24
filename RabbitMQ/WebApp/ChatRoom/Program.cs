using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ChatRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This app creates a simple chat room just type your room name and users inside this room will receive your messages.
            // To test it just start the app several times.

            Console.WriteLine("Please enter chat room name!");
            var chatRoomName = Console.ReadLine();

            Console.WriteLine("Please enter your name!");
            var username = Console.ReadLine();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var exchangeName = "chatExchange";
            var queueName = Guid.NewGuid().ToString();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
            channel.QueueDeclare(queueName, true, true, true);
            channel.QueueBind(queueName, exchangeName, chatRoomName);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventsArgs) =>
            {
                var message = Encoding.UTF8.GetString(eventsArgs.Body.ToArray());
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);

            var input = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(input))
            {
                var message = $"{username}: {input}";

                var bytes = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchangeName, chatRoomName, null, bytes);
                input = Console.ReadLine();
            }

            channel.Close();
            connection.Close();
        }
    }
}
