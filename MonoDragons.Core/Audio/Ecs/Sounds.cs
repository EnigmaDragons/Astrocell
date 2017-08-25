using System.Collections.Generic;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Audio.Ecs
{
    public sealed class Sounds : EntityComponent
    {
        private readonly List<Sound> _queued = new List<Sound>();
        private Map<string, Sound> Values { get; } = new Map<string, Sound>();

        public Sounds Add(Sound sound)
        {
            Values[sound.Name] = sound;
            return this;
        }

        public void Play(string sound)
        {
            _queued.Add(Values.ContainsKey(sound) 
                ? Values[sound] 
                : Sound.Missing);
        }

        public IEnumerable<Sound> Dequeue()
        {
            var queue = _queued.Copy();
            _queued.Clear();
            return queue;
        }
    }
}
