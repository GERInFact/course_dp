using System;

namespace _Core._Scripts.Clean_Architecture
{
    public static class EventBus<T>
    {
        public static event Action<T> Event;

        public static void Raise(T value) => Event?.Invoke(value);
    }
}