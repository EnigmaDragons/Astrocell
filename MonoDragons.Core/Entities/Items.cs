using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Entities
{
    public class Items : IList<GameObject>
    {
        private readonly List<GameObject> _items = new List<GameObject>();

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public Items() { }

        public Items(IEnumerable<GameObject> items)
        {
            _items = items.ToList();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(GameObject item)
        {
            if (Contains(item))
                Remove(item);
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(GameObject item)
        {
            return _items.Any(x => x.Id == item.Id);
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(GameObject item)
        {
            return _items.Remove(_items.First(x => x.Id == item.Id));
        }

        public int IndexOf(GameObject item)
        {
            return _items.FindIndex(x => x.Id == item.Id);
        }

        public void Insert(int index, GameObject item)
        {
            if (Contains(item))
            {
                index = IndexOf(item) >= index ? index : index - 1;
                Remove(item);
            }
            if (index == Count)
                Add(item);
            else
                _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public GameObject this[int index]
        {
            get { return _items[index]; }
            set { Insert(index, value); }
        }

        public void AddRange(IEnumerable<GameObject> items)
        {
            foreach (var item in items)
                Add(item);
        }
    }
}
