

namespace KiKaMusicPlayer.Utilities
{
    class MessageBus
    {

        static MessageBus m_instance = new();
        public static MessageBus Instance
        {
            get { return m_instance; }
        }

        private MessageBus()
        {

        }

        private readonly Dictionary<Type, List<Delegate>> m_subscriptions = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<T> callback) where T : BaseMessage
        {
            LockOperations(() =>
            {
                if (!m_subscriptions.ContainsKey(typeof(T)))
                {
                    m_subscriptions[typeof(T)] = new List<Delegate>();
                }
                m_subscriptions[typeof(T)].Add(callback);
            });
        }

        public void Unsubscribe<T>(Action<T> callback) where T : BaseMessage
        {
            LockOperations(() =>
            {
                if (m_subscriptions.ContainsKey(typeof(T)))
                {
                    m_subscriptions[typeof(T)].Remove(callback);
                }
            });
        }

        public void Publish<T>(T message) where T : BaseMessage
        {
            List<Delegate> callbacks;
            if (m_subscriptions.TryGetValue(typeof(T), out callbacks))
            {
                foreach (var callback in callbacks.ToList())
                {
                    var action = callback as Action<T>;
                    if (action != null)
                    {
                        action(message);
                    }
                }
            }
        }

        private void LockOperations(Action action)
        {
            lock (m_subscriptions)
            {
                action();
            }
        }
    }


}

