using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace ExploreCalifornia.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpPost]
        [Route("Book")]
        public IActionResult Book()
        {
            var tourname = Request.Form["tourname"];
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var needsTransport = Request.Form["transport"] == "on";

            var message = $"{tourname};{name};{email}";
            var routingKey = "tour.booked";

            // SendMessage(routingKey, message);

            var headers = new Dictionary<string, object>
            {
                { "subject", "tour" },
                { "action", "booked"}
            };

            SendMessage(headers, message);

            if (needsTransport)
            {
                // These Headers will be matched by back-office service and will receive the message.
                var needtransportHeaders = new Dictionary<string, object>
                {
                    { "subject", "transport" },
                    { "action", "booked"}
                };

                SendMessage(needtransportHeaders, message);
            }

            return Redirect($"/BookingConfirmed?tourname={tourname}&name={name}&email={email}");
        }

        [HttpPost]
        [Route("Cancel")]
        public IActionResult Cancel()
        {
            var tourname = Request.Form["tourname"];
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var cancelReason = Request.Form["reason"];

            var message = $"{tourname};{name};{email};{cancelReason}";
            var routingKey = "tour.canceled";

            // SendMessage(routingKey, message);

            var headers = new Dictionary<string, object>
            {
                { "subject", "tour" },
                { "action", "canceled"}
            };

            SendMessage(headers, message);

            return Redirect($"/BookingCanceled?tourname={tourname}&name={name}");
        }

        private void SendMessage(string routingKey, string message)
        {
            var exchangeName = "webappExchange";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchangeName, routingKey, null, bytes);

                    channel.Close();
                }

                connection.Close();
            }
        }

        // This overload of the method is for the Headers exchange type.
        private void SendMessage(Dictionary<string, object> headers, string message)
        {
            var exchangeName = "webappExchange";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    var props = channel.CreateBasicProperties();
                    props.Headers = headers;

                    channel.BasicPublish(exchangeName, string.Empty, props, bytes);

                    channel.Close();
                }

                connection.Close();
            }
        }
    }
}