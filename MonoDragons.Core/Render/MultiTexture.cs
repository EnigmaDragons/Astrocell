using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class MultiTexture : EntityComponent
    {
        public List<Texture> Textures { get; set; }

        public MultiTexture(params Texture[] textures)
        {
            Textures = textures.ToList();
        }
    }
}
