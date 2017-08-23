using System;
using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Scenes
{
    public abstract class EcsScene : IScene
    {
        private readonly List<GameObject> _objs = new List<GameObject>();

        protected abstract IEnumerable<GameObject> CreateObjs();

        protected void NavigateToScene(string sceneName)
        {
            Entity.Destroy(_objs);
            World.NavigateToScene(sceneName);
        }

        protected void NavigateToScene(IScene scene)
        {
            Entity.Destroy(_objs);
            World.NavigateToScene(scene);
        }

        public void Init()
        {
            _objs.AddRange(CreateObjs());
        }

        protected void AddObj(GameObject obj)
        {
            _objs.Add(obj);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw()
        {
        }
    }
}
