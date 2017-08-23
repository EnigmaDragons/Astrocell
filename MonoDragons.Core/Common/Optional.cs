using System;

namespace MonoDragons.Core.Common
{
    public sealed class Optional<T>
    {
        private readonly T _value;

        public bool HasValue { get; }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException($"Optional {typeof(T).Name} has no value.");
                return _value;
            }
        }

        public Optional()
        {
            HasValue = false;   
        }

        public Optional(T value)
        {
            _value = value;
            HasValue = value != null;
        }

        public override string ToString()
        {
            return HasValue ? _value.ToString() : "Nothing";
        }

        public bool IsTrue(Predicate<T> condition)
        {
            return HasValue && condition(_value);
        }

        public bool IsFalse(Predicate<T> condition)
        {
            return HasValue && !condition(_value);
        }

        public void IfPresent(Action<T> action)
        {
            if (HasValue)
                action(_value);
        }

        public static implicit operator Optional<T>(T obj)
        {
            return new Optional<T>(obj);
        }
    }
}
