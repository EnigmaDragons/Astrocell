using System;

namespace MonoDragons.Core.Entities
{
    public interface IEntities
    {
        void With<T>(Action<GameObject, T> action) where T : EntityComponent;
    }
}
