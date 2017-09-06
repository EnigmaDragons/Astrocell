namespace MonoDragons.Core.EventSystem
{
    public static class Event
    {
        private static readonly Events _instance;

        static Event()
        {
            _instance = new Events();
        }

        public static int CurrentEventSubscriptionCount => _instance.SubscriptionCount;

        public static void Publish(object payload)
        {
            _instance.Publish(payload);
        }


        public static void Subscribe(EventSubscription subscription)
        {
            _instance.Subscribe(subscription);
        }


        public static void Unsubscribe(object owner)
        {
            _instance.Unsubscribe(owner);
        }
    }
}
