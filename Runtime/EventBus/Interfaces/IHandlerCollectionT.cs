
namespace Futuclass.EventBus
{
    public interface IHandlerCollection<TEvent> : IHandlerCollection where TEvent : EventBase
    {
        void Handle(TEvent eventObject);
    }
}
