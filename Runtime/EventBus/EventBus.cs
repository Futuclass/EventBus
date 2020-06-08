using System;
using System.Collections.Generic;

namespace Futuclass.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly bool _throwIfNotRegistered;

        private readonly IDictionary<Type, IHandlerCollection> _typeToHandler = new Dictionary<Type, IHandlerCollection>();
        private readonly IDictionary<object, ISubscription> _subscriptions = new Dictionary<object, ISubscription>();
        
        public EventBus(bool throwIfNotRegistered = false)
        {
            this._throwIfNotRegistered = throwIfNotRegistered;
        }

        public void Publish<TEvent>(TEvent eventObject) where TEvent : EventBase
        {
            var handlerCollection = GetTypedHandlerCollection<TEvent>();

            if (handlerCollection != null)
            {
                handlerCollection.Handle(eventObject);
                return;
            }

            if (_throwIfNotRegistered)
            {
                var type = typeof(TEvent);
                var errorMessage = $"No handler found for published type <{type}>";
                throw new TypeNotRegisteredException(type, errorMessage);
            }
        }

        internal IHandlerCollection<TEvent> GetTypedHandlerCollection<TEvent>() where TEvent : EventBase
        {
            if (_typeToHandler.TryGetValue(typeof(TEvent), out var coll))
            {
                return coll as IHandlerCollection<TEvent>;
            }

            return null;
        }

        public ISubscription RegisterSubscription(object eventProxy)
        {
            if (_subscriptions.ContainsKey(eventProxy))
            {
                throw new AlreadyRegisteredException(eventProxy, this);
            }

            var methods = HandlerFinder.GetPublicInstanceMethods(eventProxy);

            var handlersMethodData = HandlerFinder.GetHandlersData(methods);

            var handlers = HandlerFactory.CreateHandlers(handlersMethodData, eventProxy);

            var typeAndHandler = new Dictionary<Type, List<IHandler>>();

            foreach (var handler in handlers)
            {
                if (!typeAndHandler.ContainsKey(handler.HandledType))
                {
                    typeAndHandler.Add(handler.HandledType, new List<IHandler>());
                }

                typeAndHandler[handler.HandledType].Add(handler);
            }

            var dict = new Dictionary<Type, IList<IHandler>>();
            foreach (var keyValuePair in typeAndHandler)
            {
                dict.Add(keyValuePair.Key, keyValuePair.Value);
                if (_typeToHandler.TryGetValue(keyValuePair.Key, out var handlerCollect))
                {
                    handlerCollect.AddHandlers(keyValuePair.Value);
                }
                else
                {
                    var handlCollType = typeof(HandlerCollection<>).MakeGenericType(keyValuePair.Key);
                    var handlCollection = Activator.CreateInstance(handlCollType, this) as IHandlerCollection;
                    _typeToHandler.Add(keyValuePair.Key, handlCollection);
                    handlCollection?.AddHandlers(keyValuePair.Value);
                }

            }

            var sub = new Subscription(eventProxy, this, dict);
            _subscriptions.Add(eventProxy, sub);
            return sub;
        }

        public void RemoveSubscription(ISubscription subscription)
        {
            if (subscription == null) return;

            if (!_subscriptions.ContainsKey(subscription.EventProxy)) return;

            _subscriptions.Remove(subscription.EventProxy);

            var types = subscription.Handlers.Keys;

            foreach (var type in types)
            {
                _typeToHandler[type].RemoveSubscription(subscription);
            }
        }

        public void RemoveType(Type type)
        {
            _typeToHandler.Remove(type);
        }
    }
}
