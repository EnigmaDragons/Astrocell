using System;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Inputs
{
    public sealed class Directable : EntityComponent
    {
        public Action<Direction> Binding { get; set; }

        public Directable(Action<Direction> binding)
        {
            Binding = binding;
        }
    }
}
