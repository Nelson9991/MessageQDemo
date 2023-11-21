using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageQDemo.RabbitMq;

public class RabbitMqMessageSender : IRabbitMqMessageSender
{
  private readonly string _hostName;
  private readonly string _userName;
  private readonly string _password;
  private IConnection? _connection = default;

  public RabbitMqMessageSender()
  {
    _hostName = "localhost";
    _userName = "guest";
    _password = "guest";
  }

  public void SendMessage(object message, string exchangeName)
  {
    if (ConnectionExists())
    {
      using var chanel = _connection!.CreateModel();
      chanel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: false);

      var json = JsonSerializer.Serialize(message);
      var body = Encoding.UTF8.GetBytes(json);
      chanel.BasicPublish(exchange: exchangeName, "", body: body);
    }
  }

  private void CreateConnection()
  {
    try
    {
      var factory = new ConnectionFactory
      {
        HostName = _hostName,
        Password = _password,
        UserName = _userName
      };

      _connection ??= factory.CreateConnection();
    }
    catch (System.Exception)
    {
    }
  }

  private bool ConnectionExists()
  {
    if (_connection != null)
      return true;

    CreateConnection();
    return true;
  }
}
