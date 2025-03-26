using System.Text;
using System.Text.Json;
using LibFlow.Services.Email;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LibFlow.Services.RabbitMq
{
    public class BookReservationConsumer
    {
        private readonly ISendEmail _emailService;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "book_reservation_queue";

        public BookReservationConsumer(ISendEmail emailService)
        {
            _emailService = emailService;

            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var reservation = JsonSerializer.Deserialize<BookReservationMessage>(message);

                Console.WriteLine($"Recebido: {reservation.BookName} para {reservation.Email}");

                // Enviar e-mail de confirmação
                await _emailService.SendEmailAsync(reservation.Email, $"Livro {reservation.BookName} reservado", $"Seu livro {reservation.BookName} foi reservado com sucesso!");

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
        }
    }

    public class BookReservationMessage
    {
        public string BookName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
