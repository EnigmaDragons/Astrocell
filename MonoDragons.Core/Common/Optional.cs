using System;

namespace MonoDragons.Core.Common
{
    public sealed class Optional<T>
    {
        private readonly T _value;

        public bool IsPresent { get; }

        public T Value
        {
            get
            {
                if (!IsPresent)
                    throw new InvalidOperationException($"Optional {typeof(T).Name} has no value.");
                return _value;
            }
        }

        public Optional()
        {
            IsPresent = false;
        }

        public Optional(T value)
        {
            _value = value;
            IsPresent = value != null;
        }

        public override string ToString()
        {
            return IsPresent ? _value.ToString() : "Nothing";
        }

        public bool IsTrue(Predicate<T> condition)
        {
            return IsPresent && condition(_value);
        }

        public bool IsFalse(Predicate<T> condition)
        {
            return IsPresent && !condition(_value);
        }

        public void IfPresent(Action<T> action)
        {
            if (IsPresent)
                action(_value);
        }

        public static implicit operator Optional<T>(T obj)
        {
            return new Optional<T>(obj);
        }
    }
}
