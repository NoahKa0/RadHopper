using Microsoft.Extensions.Logging;
using RadHopper.Consumers;

namespace RadHopper.Samples.Clover
{
    internal sealed class PingConsumer(ILogger<PingConsumer> _logger) : IConsumer<PingMessage>
    {
        public Task Consume(HopMessage<PingMessage> message)
        {
            _logger.LogInformation("Ping");

            return Task.CompletedTask;
        }
    }
}
