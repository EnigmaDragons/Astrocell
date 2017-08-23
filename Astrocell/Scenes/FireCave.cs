using System.Collections.Generic;
using System.IO;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.Tiled.Orthographic;
using MonoDragons.Core.Tiled.TmxLoading;

namespace Astrocell.Scenes
{
    public class FireCave : EcsScene
    {
        protected override IEnumerable<GameObject> CreateObjs()
        {
            return new TileMapFactory().CreateMap(Tmx.Create(Path.Combine("Maps", "FireCave.tmx")));
        }
    }
}
