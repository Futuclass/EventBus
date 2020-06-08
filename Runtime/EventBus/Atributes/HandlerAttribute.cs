using System;

namespace Futuclass.EventBus
{
    /// <summary>
    /// All handler functions need to be marked with this attribute for reflection lookup
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HandlerAttribute : Attribute
    {
        public HandlerPriority Priority { get; }

        public HandlerAttribute(HandlerPriority priority)
        {
            Priority = priority;
        }
    }
}