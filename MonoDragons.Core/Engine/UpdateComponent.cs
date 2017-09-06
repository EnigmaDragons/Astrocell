using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Engine
{
    public abstract class UpdateComponent : EntityComponent
    {
        public abstract void Update(TimeSpan delta);
    }
}
