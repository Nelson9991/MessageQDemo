using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.UI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailSenderApi.Messaging;

public class RabbitMqEmailConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName = string.Empty;

    public RabbitMqEmailConsumer(IEmailSender emailSender, IConfiguration configuration)
    {
        _emailSender = emailSender;
        _configuration = configuration;
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(Sd.EMAIL_QUEUE, ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(_queueName, Sd.EMAIL_QUEUE, "");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var emailDto = JsonSerializer.Deserialize<EmailDto>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });

            await _emailSender.SendEmailAsync(emailDto!.To, "Email desde RabbitMQ", emailDto.Message);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, false, consumer);

        return Task.CompletedTask;
    }
}
