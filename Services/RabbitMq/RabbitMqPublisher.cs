using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using LibFlow.Services.RabbitMq;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly ConnectionFactory _factory;
    private readonly string _exchangeName;
    private readonly string _routingKey;
    private readonly string _queueName;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");

        _factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            Port = int.Parse(rabbitMqConfig["Port"]),
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"]
        };

        _exchangeName = rabbitMqConfig["ExchangeName"];
        _routingKey = rabbitMqConfig["RoutingKey"];
        _queueName = rabbitMqConfig["QueueName"];
    }

    public void PublishBookReservation(string bookName, string userEmail)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Declaração da Exchange e da Fila (garante que existem)
        channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);
        channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(_queueName, _exchangeName, _routingKey);

        // Criando mensagem JSON
        var message = new
        {
            BookName = bookName,
            UserEmail = userEmail
        };

        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true; // Garante que a mensagem não será perdida

        // Publicando mensagem
        channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: _routingKey,
            basicProperties: properties,
            body: messageBody
        );

        Console.WriteLine($"[x] Mensagem enviada: Livro '{bookName}' reservado para '{userEmail}'");
    }
}
