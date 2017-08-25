using System;

namespace MonoDragons.Core.Entities
{
    public abstract class EntityComponent
    {
        private GameObject _parent;

        public GameObject GameObject
        {
            get
            {
                if (_parent == null) 
                    throw new InvalidOperationException("GameObject cannot be accessed on a released or uninitialize component.");
                return _parent;
            }
        }

        public void Init(GameObject parent)
        {
            _parent = parent;
        }

        public void Release()
        {
            _parent = null;
        }
    }
}
