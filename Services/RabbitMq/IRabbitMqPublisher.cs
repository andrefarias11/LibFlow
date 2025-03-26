namespace LibFlow.Services.RabbitMq;

public interface IRabbitMqPublisher
{
    void PublishBookReservation(string bookName, string userEmail);
}