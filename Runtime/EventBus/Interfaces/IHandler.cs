using System;

namespace Futuclass.EventBus
{
    public interface IHandler
    {
        Type HandledType { get; }

        ISubscription Subscription { get; set; }

        HandlerPriority Priority { get; }
    }
}
