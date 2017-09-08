using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Development
{
    public class PressEscapeToQuit : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Escape))
                GameInstance.TheGame.Exit();
        }
    }
}
