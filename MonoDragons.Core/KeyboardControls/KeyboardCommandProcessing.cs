using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public sealed class KeyboardCommandProcessing : ISystem
    {
        private List<Keys> _keysDown = new List<Keys>();

        public void Update(IEntities entities, TimeSpan delta)
        {
            var newDownKeys = Keyboard.GetState().GetPressedKeys().ToList();
            var newlyPressedKeys = newDownKeys.Where(x => !_keysDown.Contains(x));

            entities.With<KeyboardCommand>(
                x => newlyPressedKeys.ForEach(x.NotifyKeyPressed));

            _keysDown = newDownKeys;
        }
    }
}
