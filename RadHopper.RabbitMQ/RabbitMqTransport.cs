using RabbitMQ.Client;
using RadHopper.Abstractions;
using RadHopper.RabbitMQ.Configuration;
using RadHopper.Transport;
using RadHopper.Transport.Exceptions;

namespace RadHopper.RabbitMQ;

public class RabbitMqTransport : ITransportLayer
{
    private readonly ITransportConfigurator _config;
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;

    private readonly List<IConsumerDescriptor> _consumers = [];
    private readonly List<Func<IServiceProvider, IConsumerDescriptor>> _consumerDescriptorFactories = [];

    internal RabbitMqTransport(RabbitMqTransportConfigurator configurator)
    {
        _config = configurator.Configurator;
        _factory = configurator.CreateConnectionFaactory();
        _consumerDescriptorFactories.AddRange(configurator.ConsumerDescriptorFactories);
    }

    public RabbitMqTransport(ITransportConfigurator config, ConnectionFactory factory)
    {
        _config = config;
        _factory = factory;
    }

    async Task ITransportLayer.SetupConnection(IServiceProvider sp)
    {
        if (_connection != null)
            throw new ConnectorException("Already initialized");
        _connection = await _factory.CreateConnectionAsync();
        
        foreach (var action in _consumerDescriptorFactories)
        {
            var descriptor = action(sp);

            _consumers.Add(descriptor);
        }

        _consumerDescriptorFactories.Clear();

        foreach (var consumer in _consumers)
        {
            await consumer.SetupConnection(_connection, sp, _config);
        }
    }

    public IPublisher GetPublisher()
    {
        return new RabbitMqPublisher(_factory);
    }
}