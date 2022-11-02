
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("RabbitMQ Default Exchange Publisher");

var counter = 0;
do
{
    int timeToSleep = new Random().Next(1000, 3000);
    Thread.Sleep(timeToSleep);

    var factory = new ConnectionFactory { HostName = "localhost" };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare(queue: "dev-queue",
                         durable: false,
                         exclusive: false,
                         arguments: null);

    string message = $"Message from Publisher #{counter++}";

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "",
                         routingKey: "dev-queue",
                         basicProperties: null,
                         body: body);

    Console.WriteLine($"Message #{counter} is sent to Default exchange");
}
while (true);