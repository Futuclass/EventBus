using System;

namespace Futuclass.EventBus
{
    public interface IHandler<TEvent> : IHandler where TEvent : EventBase
    {
        /// <summary>
        /// Handler action
        /// </summary>
        Action<TEvent> Action { get; }

        /// <summary>
        /// Called when appropriate message is being dispatched
        /// </summary>
        /// <param name="eventObj"></param>
        void Handle(TEvent eventObj);
    }
}
