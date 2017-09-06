using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Scenes;

namespace MonoDragons.Core.Navigation
{
    public sealed class SceneFactory
    {
        private readonly Dictionary<string, Func<IScene>> _sceneInstructions;

        public SceneFactory(Dictionary<string, Func<IScene>> sceneInstructions)
        {
            _sceneInstructions = sceneInstructions.ToDictionary(x => x.Key.ToLowerInvariant(), x => x.Value);
        }

        public IScene Create(string sceneName)
        {
            return _sceneInstructions[sceneName.ToLowerInvariant()]();
        }

        public bool Exists(string sceneName)
        {
            return _sceneInstructions.ContainsKey(sceneName.ToLowerInvariant());
        }
    }
}
