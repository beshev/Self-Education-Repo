using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreCalifornia.BackOffice
{
    internal class BackOfficeConsumer
    {
        static void Main(string[] args)
        { 
            // This program will consume the messages from the RabbitMQ broker.
            // You must specify the way that queue will bind, depending on the exchange type created at the web app.

            var queueName = "backOfficeQueue";
            var exchangeName = "webappExchange";
            var routingKey = "tour.*";

            // This headers are used for Headers exchange and ignore routing key.
            var headers = new Dictionary<string, object>
            {
                { "subject", "tour" },
                { "action", "booked"},
                // This header makes a rule that aany exchange headers must be equal to any queue bind headers.
                {"x-match", "any" }
            };

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, true, false, false);
                    channel.QueueBind(queueName, exchangeName, string.Empty, headers);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, eventArgs) =>
                    {
                        var msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                        // Console.WriteLine($"{eventArgs.RoutingKey} -> {msg}");

                        var subject = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["subject"] as byte[]);
                        var action = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["action"] as byte[]);
                        Console.WriteLine($"{subject}: {action} -> {msg}");
                    };

                    channel.BasicConsume(queueName, true, consumer);

                    Console.ReadLine();

                    channel.Close();
                }

                connection.Close();
            }
        }
    }
}
