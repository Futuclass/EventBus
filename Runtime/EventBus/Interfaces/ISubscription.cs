using System;
using System.Collections.Generic;
using Futuclass.EventBus;

namespace Futuclass.EventBus
{
    public interface ISubscription : IDisposable
    {
        /// <summary>
        /// Object used for handler lookup
        /// </summary>
        object EventProxy { get; }

        /// <summary>
        /// Event bus, that this subscription belong to
        /// </summary>
        IEventBus EventBus { get; }

        /// <summary>
        /// Defines, if handlers are active (It is not restrictive, should check that inside handler manually)
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// All registered handlers
        /// </summary>
        IDictionary<Type, IList<IHandler>> Handlers { get; }
    }
}