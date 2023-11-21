namespace MessageQDemo.RabbitMq;

public interface IRabbitMqMessageSender
{
  void SendMessage(object message, string exchangeName);
}
