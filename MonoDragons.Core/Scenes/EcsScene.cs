using System.Collections.Generic;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Scenes
{
    public abstract class EcsScene : IScene
    {
        private readonly List<GameObject> _objs = new List<GameObject>();

        protected abstract IEnumerable<GameObject> CreateObjs();

        public void Init()
        {
            _objs.AddRange(CreateObjs());
        }

        public void Dispose()
        {
            Entity.Destroy(_objs);
        }

        protected void AddObj(GameObject obj)
        {
            _objs.Add(obj);
        }
    }
}
