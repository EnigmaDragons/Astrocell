using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoDragons.TiledEditor.Maps;

namespace MonoDragons.TiledEditor.Events
{
    public class MapEvents
    {
        private readonly JsonIo _json = new JsonIo();
        private readonly Dictionary<string, IMapEventType> _eventTypes = new Dictionary<string, IMapEventType>();
        private readonly string _mapPath;
        private readonly Lazy<Dictionary<string, List<MapEvent>>> _events;

        public MapEvents(string mapPath, params IMapEventType[] eventTypes)
        {
            _mapPath = mapPath;
            _events = new Lazy<Dictionary<string, List<MapEvent>>>(Load);
            eventTypes.ForEach(eventType => _eventTypes[eventType.TypeName] = eventType);
        }

        public void Add(MapEvent mapEvent)
        {
            if (!_eventTypes.ContainsKey(mapEvent.TypeName))
                throw new ArgumentException("Map events must have key that maps to a map event type.");
            InsertEvent(mapEvent, _events.Value);
            _json.Save(_mapPath, _events.Value.Values.SelectMany(eventList => eventList));
        }

        public void Remove(MapEvent mapEvent)
        {
            _events.Value[mapEvent.Position.ToString()].Remove(mapEvent);
            _json.Save(_mapPath, _events.Value.Values.SelectMany(eventList => eventList));
        }

        public IEnumerable<MapEvent> GetTileEvents(TilePosition tilePostion)
        {
            return _events.Value.ContainsKey(tilePostion.ToString()) 
                ? _events.Value[tilePostion.ToString()] 
                : new List<MapEvent>();
        }

        public IEnumerable<GameObject> InstantiateEvents()
        {
            return _events.Value.Values.SelectMany(eventList => eventList.Select(e => _eventTypes[e.TypeName].Instantiate(e)));
        }

        private Dictionary<string, List<MapEvent>> Load()
        {
            var result = new Dictionary<string, List<MapEvent>>();
            if (File.Exists(_mapPath))
                _json.Load<List<MapEvent>>(_mapPath).ForEach(e => InsertEvent(e, result));
            return result;
        }

        private void InsertEvent(MapEvent mapEvent, Dictionary<string, List<MapEvent>> events)
        {
            if (!events.ContainsKey(mapEvent.Position.ToString()))
                events[mapEvent.Position.ToString()] = new List<MapEvent>();
            events[mapEvent.Position.ToString()].Add(mapEvent);
        } 
    }
}
