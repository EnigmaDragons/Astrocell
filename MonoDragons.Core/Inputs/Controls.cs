using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Inputs
{
    public sealed class Controls : EntityComponent
    {
        public Map<Control, Action> Bindings { get; set; }

        public Controls(Map<Control, Action> bindings)
        {
            Bindings = bindings;
        }

        public void OnControl(Control control)
        {
            if (Bindings.ContainsKey(control))
                Bindings[control]();
        }
    }
}
