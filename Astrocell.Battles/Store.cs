using System;

namespace Astrocell.Battles
{
    public sealed class Store<T>
    {
        private T _value;

        public bool IsEmpty => _value == null;

        public T Get()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Store contains no value.");
            return _value;
        }

        public void Put(T value)
        {
            _value = value;
        }
    }
}
