using System;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public sealed class KeyboardCommand : EntityComponent
    {
        public Action Command { get; set; } = () => { };
        public Keys Key { get; set; } = Keys.None;

        public void NotifyKeyPressed(Keys key)
        {
            if (Key == key)
                Command();
        }
    }
}
