using System;
using System.Collections.Generic;
using System.Linq;
using Futuclass.EventBus;

namespace Futuclass.EventBus
{
    internal static class HandlerFactory
    {
        internal static IEnumerable<IHandler> CreateHandlers(IEnumerable<HandlerMethodData> handlersData, object eventProxy)
        {
            return handlersData.Select(handlerData => CreateHandler(handlerData, eventProxy)).ToList();
        }

        internal static IHandler CreateHandler(HandlerMethodData handlerData, object eventProxy)
        {
            var paramType = handlerData.ParameterType;

            // Creates Handler<TEvent> type using TEvent from method parameter type
            var handlerType = typeof(Handler<>).MakeGenericType(paramType);

            // Creates Action<T> type using T from method parameter type
            var actionType = typeof(Action<>).MakeGenericType(paramType);
            
            // Creates instance of Action<T> using T from method parameter type
            var action = Delegate.CreateDelegate(actionType, eventProxy, handlerData.Method);
            
            // Creates instance of Handler<TEvent> using TEvent param type , handler priority from attribute and action from MethodInfo
            var handler = Activator.CreateInstance(handlerType, paramType, handlerData.AttachedAttribute.Priority, action);

            return handler as IHandler;
        }
    }
}
