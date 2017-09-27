using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public sealed class KeyboardCommands : EntityComponent
    {
        public Map<Keys, Action> Commands { get; set; } = new Map<Keys, Action>();

        public void NotifyKeyPressed(Keys key)
        {
            if (Commands.ContainsKey(key))
                Commands[key].Invoke();
        }

        public KeyboardCommands Add(Keys keys, Action action)
        {
            Commands[keys] = action;
            return this;
        }
    }
}
