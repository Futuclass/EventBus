using UnityEngine;

namespace Futuclass.EventBus.Unity
{
    public class MonoBehaviourProxy : MonoBehaviour, IProxy
    {
        public ISubscription Subscription { get; protected set; }

        protected virtual void Awake()
        {
            Subscription = GlobalEventBus.Instance.RegisterSubscription(this);
        }

        protected virtual void OnEnable()
        {
            if (Subscription != null)
                Subscription.Active = true;
        }

        protected virtual void OnDisable()
        {
            if (Subscription != null) 
                Subscription.Active = false;
        }

        protected virtual void OnDestroy()
        {
            Subscription.Dispose();
        }
    }
}