using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;

namespace Astrocell.Scenes
{
    class EcsDisposeScene : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            yield return Entity.Create(new Transform2(new Vector2(300, 300), Rotation2.Up, new Size2(300, 300), 1))
                .Add((o, r) => new Texture(r.LoadTexture("sprites/gareth-face.png", o)))
                .Add(o => new MouseClickListener { OnClick = x => Entity.Destroy(o)});
        }
    }
}
