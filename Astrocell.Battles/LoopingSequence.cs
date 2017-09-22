using System.Collections.Generic;

namespace Astrocell.Battles
{
    public sealed class LoopingSequence<T>
    {
        private readonly List<T> _items;

        public IList<T> Items => _items.AsReadOnly();

        public T this[int index] => _items[index];

        public LoopingSequence(List<T> items)
        {
            _items = items;
        }

        public T Current => _items[0];

        public T Next()
        {
            var oldFirst = _items[0];
            _items.RemoveAt(0);
            _items.Add(oldFirst);
            return _items[0];
        }
    }
}
