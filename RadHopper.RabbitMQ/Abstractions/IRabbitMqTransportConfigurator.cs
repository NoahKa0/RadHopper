using RadHopper.Consumers;

namespace RadHopper.RabbitMQ.Abstractions
{
    public interface IRabbitMqTransportConfigurator
    {
        IRabbitMqTransportConfigurator AddReceiveEndpoint<C, TM>(string? queueName = null)
            where C : IConsumerRoot<TM>
            where TM : class;

        IRabbitMqTransportConfigurator WithHost(string host);
    }
}
