using RadHopper.Abstractions;
using RadHopper.RabbitMQ.Abstractions;
using RadHopper.RabbitMQ.Configuration;
using RadHopper.Transport;

namespace RadHopper.RabbitMQ.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ITransportLayer UsingRabbitMQ<T>(this T configurator, Action<IRabbitMqTransportConfigurator> callback) where T : ITransportConfigurator
        {
            var transportConfigurator = new RabbitMqTransportConfigurator(configurator);

            callback.Invoke(transportConfigurator);

            return new RabbitMqTransport(transportConfigurator);
        }
    }
}
