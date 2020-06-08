using System;
using System.Collections.Generic;
using Futuclass.EventBus;

namespace Futuclass.EventBus
{
    internal class Subscription : ISubscription
    {
        /// <summary>
        /// Object used for handler lookup
        /// </summary>
        public object EventProxy { get; }

        /// <summary>
        /// Event bus, that this subscription belong to
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Defines, if handlers are active (It is not restrictive, should check that inside handler manually)
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// All registered handlers
        /// </summary>
        public IDictionary<Type, IList<IHandler>> Handlers { get; }


        public Subscription(object eventProxy, IEventBus evBus, IDictionary<Type, IList<IHandler>> handlers)
        {
            EventProxy = eventProxy;
            EventBus = evBus;
            Handlers = handlers;

            foreach (var handlerCollection in handlers.Values)
            {
                foreach (var handler in handlerCollection)
                {
                    handler.Subscription = this;
                }
            }
        }

        /// <summary>
        /// Removes subscription from event bus
        /// </summary>
        public void Dispose()
        {
            EventBus.RemoveSubscription(this);
        }
    }
}
