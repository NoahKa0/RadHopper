using RadHopper.Abstractions;
using RadHopper.Attributes;
using RadHopper.Consumers.BehaviorFactory;

namespace RadHopper.Transport;

public sealed class TransportConfigurator : ITransportConfigurator
{
    private readonly IBehaviorFactory BehaviorFactory;
    internal TransportConfigurator(IBehaviorFactory behaviorFactory)
    {
        BehaviorFactory = behaviorFactory;
    }
    
    public int? DefaultBatchSize { get; set; }
    public int? DefaultWaitTimeMs { get; set; }
    public bool RequeueOnError { get; set; } = true;
    public bool NeverDiscard { get; set; } = false;

    public int GetBatchSize(Type consumerType)
    {
        var config = consumerType
            .GetCustomAttributes(typeof(ConsumerConfigurationAttribute), true)
            .Cast<ConsumerConfigurationAttribute>()
            .FirstOrDefault();

        var result = config?.BatchSize ?? DefaultBatchSize ?? Environment.ProcessorCount;
        return result > 0 ? result : 1;
    }
    
    public int GetWaitTimeMs(Type consumerType)
    {
        var config = consumerType
            .GetCustomAttributes(typeof(ConsumerConfigurationAttribute), true)
            .Cast<ConsumerConfigurationAttribute>()
            .FirstOrDefault();

        var result = config?.WaitTimeMs ?? DefaultWaitTimeMs ?? 1000;
        return result > 0 ? result : 1000;
    }
    
    IBehaviorFactory ITransportConfigurator.BehaviorFactory => BehaviorFactory;
}