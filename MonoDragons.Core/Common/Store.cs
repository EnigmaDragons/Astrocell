﻿using System;

namespace MonoDragons.Core.Common
{
    public sealed class Store<T>
    {
        private T _value;

        public bool IsEmpty => _value == null;

        public Store() 
            : this(default(T)) { }

        public Store(T defaultValue)
        {
            _value = defaultValue;
        }

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

        public void Clear()
        {
            _value = default(T);
        }
    }
}
