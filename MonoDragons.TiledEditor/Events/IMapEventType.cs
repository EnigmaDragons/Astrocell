using MonoDragons.Core.Entities;

namespace MonoDragons.TiledEditor.Events
{
    public interface IMapEventType
    {
        string TypeName { get; }
        GameObject Instantiate(MapEvent mapEvent);
    }
}
