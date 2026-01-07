
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RadHopper.DependencyInjection;
using RadHopper.RabbitMQ.DependencyInjection;
using RadHopper.Samples.Clover;

var host = Host.CreateApplicationBuilder();

host.Services.AddHostedService<PingProducer>();

host.Services.AddRadHopper(configurator =>
{
    return configurator.UsingRabbitMQ(configurator =>
    {
        configurator.WithHost("localhost");

        configurator.AddReceiveEndpoint<PingConsumer, PingMessage>();
    });
});

var app = host.Build();

await app.RunAsync();