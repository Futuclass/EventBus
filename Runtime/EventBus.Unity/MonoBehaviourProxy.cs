using UnityEngine;

namespace Futuclass.EventBus.Unity
{
    public class MonoBehaviourProxy : MonoBehaviour, IProxy
    {
        public ISubscription Subscription { get; private set; }

        private void Awake()
        {
            Subscription = GlobalEventBus.Instance.RegisterSubscription(this);
        }

        private void OnEnable()
        {
            if (Subscription != null)
                Subscription.Active = true;
        }

        private void OnDisable()
        {
            if (Subscription != null) 
                Subscription.Active = false;
        }

        private void OnDestroy()
        {
            Subscription.Dispose();
        }
    }
}