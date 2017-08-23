using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Engine
{
    public sealed class Automation : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<IAutomaton>(x => x.Update(delta));
        }
    }
}
