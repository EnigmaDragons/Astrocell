using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.IO;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoDragons.TiledEditor.Events
{
    public class MapEvents
    {
        private readonly JsonIo _json = new JsonIo();
        private readonly Dictionary<string, IMapEventType> _eventTypes = new Dictionary<string, IMapEventType>();
        private readonly string _mapPath;
        private readonly Lazy<List<MapEvent>> _events;

        public MapEvents(string mapPath, params IMapEventType[] eventTypes)
        {
            _mapPath = mapPath;
            _events = new Lazy<List<MapEvent>>(() => Load());
            eventTypes.ForEach(eventType => _eventTypes[eventType.TypeName] = eventType);
        }

        public void Add(MapEvent mapEvent)
        {
            if (!_eventTypes.ContainsKey(mapEvent.TypeName))
                throw new ArgumentException("Map events must have key that maps to a map event type.");
            _events.Value.Add(mapEvent);
            _json.Save(_mapPath, _events.Value);
        }

        public void Remove(MapEvent mapEvent)
        {
            _events.Value.Remove(mapEvent);
            _json.Save(_mapPath, _events.Value);
        }

        public IEnumerable<MapEvent> GetAllTouching(Transform2 position)
        {
            return _events.Value.Where(e => e.Position.Intersects(position));
        }

        public IEnumerable<GameObject> InstantiateEvents()
        {
            return _events.Value.Select(e => _eventTypes[e.TypeName].Instantiate(e));
        }

        private List<MapEvent> Load()
        {
            if (File.Exists(_mapPath))
                return _json.Load<List<MapEvent>>(_mapPath);
            return new List<MapEvent>();
        }
    }
}
