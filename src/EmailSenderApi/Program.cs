using EmailSenderApi.Helpers;
using EmailSenderApi.Messaging;
using EmailSenderApi.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettingsSection = builder.Configuration.GetSection("MailJet");

builder.Services.Configure<MailJetOptions>(appSettingsSection);
builder.Services.AddSingleton<IEmailSender, MailJetEmailSender>();
builder.Services.AddHostedService<RabbitMqEmailConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/sendEmail", () =>
{
  return Results.Ok("Email sent");
})
.WithName("Send Email")
.WithOpenApi();

app.Run();
