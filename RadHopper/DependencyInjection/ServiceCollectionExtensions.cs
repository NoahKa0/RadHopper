using Microsoft.Extensions.DependencyInjection;
using RadHopper.Abstractions;
using RadHopper.Consumers.BehaviorFactory;
using RadHopper.Transport;

namespace RadHopper.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRadHopper(this IServiceCollection collection, Func<ITransportConfigurator, ITransportLayer> configure)
    {
        var config = new TransportConfigurator(new BehaviorFactory());

        var transport = configure(config);
        
        RadHopperHostedService.AddTransport(transport);
        
        collection.AddHostedService<RadHopperHostedService>();

        collection.AddSingleton<IPublisher>((p) => transport.GetPublisher());

        return collection;
    }
}