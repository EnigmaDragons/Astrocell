using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.KeyboardControls;
using MonoDragons.Core.Render;
using MonoDragons.Core.Render.Viewports;

namespace Astrocell.Plugins
{
    public sealed class MakeCameraFollowPlayer : ISystem
    {
        private readonly Vector2 _offset;

        public MakeCameraFollowPlayer(Vector2 offset)
        {
            _offset = offset;
        }

        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<TopDownMovement>((p, m) => 
                entities.With<Camera>((o, c) => o.Local.Location = p.World.Location - _offset));
        }
    }
}
