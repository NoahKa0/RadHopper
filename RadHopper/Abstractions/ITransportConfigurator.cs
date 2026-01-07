using RadHopper.Consumers.BehaviorFactory;

namespace RadHopper.Abstractions
{
    public interface ITransportConfigurator
    {
        bool RequeueOnError { get; }
        bool NeverDiscard { get; }
        internal IBehaviorFactory BehaviorFactory { get; }

        internal int GetBatchSize(Type consumerType);
        internal int GetWaitTimeMs(Type consumerType);
    }
}
