using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Futuclass.EventBus
{
    internal static class HandlerFinder
    {
        internal static MethodInfo[] GetPublicInstanceMethods(object eventProxy)
        {
            var proxyType = eventProxy.GetType();

            return proxyType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        }

        internal static List<HandlerMethodData> GetHandlersData(MethodInfo[] methodInfo)
        {
            var rt = new List<HandlerMethodData>();

            foreach (var method in methodInfo)
            {
                if (TryCreateHandlerData(method, out var data)) rt.Add(data);
            }

            return rt;
        }

        internal static bool TryCreateHandlerData(MethodInfo methodInfo, out HandlerMethodData data)
        {
            //No attribute found, handler cannot be created
            if (!(methodInfo.GetCustomAttributes(typeof(HandlerAttribute), true).FirstOrDefault() is HandlerAttribute attribute))
            {
                data = null;
                return false;
            }

            var methodParams = methodInfo.GetParameters();

            //Method doesn't mach handler pattern, handler cannot be created
            if (methodParams.Length != 1)
            {
                data = null;
                return false;
            }


            var methodParameter = methodParams.FirstOrDefault();

            var paramType = methodParameter?.ParameterType;

            var handlerData = new HandlerMethodData()
            {
                AttachedAttribute = attribute,

                Method = methodInfo,

                ParameterType = paramType
            };

            data = handlerData;

            return true;
        }
    }
}
