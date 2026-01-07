using RadHopper.Abstractions;

namespace RadHopper.Consumers.BehaviorFactory;

internal interface IBehaviorFactory
{
    internal IConsumerBehavior<TM> Create<C, TM>(IServiceProvider serviceProvider, ITransportConfigurator config)
        where C : IConsumerRoot<TM>
        where TM : class;
}