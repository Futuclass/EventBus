namespace Futuclass.EventBus.Unity
{
    public class GlobalEventBus : EventBus
    {
        public static GlobalEventBus Instance => _instance ?? (_instance = new GlobalEventBus());

        private static GlobalEventBus _instance;

        private GlobalEventBus(bool throwIfNotRegistered = false) : base(throwIfNotRegistered) { }

        public new static void Publish<TEvent>(TEvent eventBase) where TEvent : EventBase
        {
            ((EventBus) Instance).Publish(eventBase);
        }
    }
}