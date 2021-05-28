using System.Collections.Generic;
using System.Linq;

namespace Futuclass.EventBus
{
    internal class HandlerCollection<TEvent> : IHandlerCollection<TEvent> where TEvent : EventBase
    {
        public IEventBus EvBus { get; }

        private IList<IHandler<TEvent>> _handlers = new List<IHandler<TEvent>>();

#if ENABLE_IL2CPP
        [Preserve]
#endif
        public HandlerCollection(IEventBus evBus)
        {
            this.EvBus = evBus;
        }

#if ENABLE_IL2CPP
        [Preserve]
#endif
        public HandlerCollection(IEventBus evBus, IList<IHandler<TEvent>> handlers) : this(evBus)
        {
            this._handlers = handlers;
        }

        public void Handle(TEvent eventObject)
        {
            for (var i = 0; i < _handlers.Count; i++)
                _handlers[i]?.Handle(eventObject);
        }

        public void RemoveSubscription(ISubscription subscription)
        {
            var handlersToRemove = subscription.Handlers[typeof(TEvent)];

            //Copy handlers over, to prevent handler collection change upon removal
            var modifiedHandlerCollection = new List<IHandler<TEvent>>(_handlers);

            for (var i = 0; i < modifiedHandlerCollection.Count; i++)
            {
                foreach (var t in handlersToRemove)
                {
                    if (t as IHandler<TEvent> == _handlers[i])
                    {
                        modifiedHandlerCollection.RemoveAt(i);
                    }
                }
            }
            if (_handlers.Count == 0) EvBus.RemoveType(typeof(TEvent));
            else
                _handlers = modifiedHandlerCollection;
        }

        public void AddHandlers(IList<IHandler> list)
        {
            var casted = list.Cast<IHandler<TEvent>>().ToList();

            foreach (var handler in casted)
            {
                _handlers.Add(handler);
            }

            _handlers = _handlers.OrderByDescending(h => h.Priority).ToList();
        }
    }
}
