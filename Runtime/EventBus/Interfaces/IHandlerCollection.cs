using System.Collections.Generic;

namespace Futuclass.EventBus
{
    public interface IHandlerCollection
    {
        IEventBus EvBus { get; }

        void AddHandlers(IList<IHandler> list);
        void RemoveSubscription(ISubscription subscription);
    }
}
