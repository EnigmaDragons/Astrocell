using System;

namespace MonoDragons.Core.Entities
{
    public interface IEntities
    {
        void With<T>(int entityId, Action<GameObject, T> action);
        void With<T>(Action<GameObject, T> action);
    }
}
