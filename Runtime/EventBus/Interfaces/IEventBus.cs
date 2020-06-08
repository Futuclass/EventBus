using System;

namespace Futuclass.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// Registerer an object to event bus
        /// </summary>
        /// <param name="eventProxy">Object, which contains handlers marked with Futuclass.EventBus.HandlerAttribute />
        /// </param>
        /// <returns></returns>
        ISubscription RegisterSubscription(object eventProxy);

        /// <summary>
        /// Publish event data to event bus
        /// </summary>
        /// <typeparam name="TEvent">Type of event occured</typeparam>
        /// <param name="eventObject">Event data</param>
        void Publish<TEvent>(TEvent eventObject) where TEvent : EventBase;

        /// <summary>
        /// Deletes a subscription from event bus
        /// </summary>
        /// <param name="subscription"></param>
        void RemoveSubscription(ISubscription subscription);

        /// <summary>
        /// Removes all handlers, that accept particular event type
        /// </summary>
        /// <param name="type"></param>
        void RemoveType(Type type);
    }
}