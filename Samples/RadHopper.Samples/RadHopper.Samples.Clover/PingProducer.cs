using Microsoft.Extensions.Hosting;
using RadHopper.Transport;

namespace RadHopper.Samples.Clover
{
    internal sealed class PingProducer(IPublisher _publisher) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1_000, stoppingToken);

                await _publisher.Publish(new PingMessage(), stoppingToken);
            }
        }
    }
}
