using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Tiled.Orthographic
{
    public sealed class Tile : EntityComponent
    {
        private readonly string _texturePath;

        private Texture2D _texture;

        public Rectangle SourceRect { get; }
        public int ZIndex { get; }

        public Tile(TileDetail detail, int zIndex)
        {
            _texturePath = detail.Texture;
            SourceRect = detail.SourceRect;
            ZIndex = zIndex;
        }


        public Texture2D Texture
        {
            get
            {
                if (_texture == null)
                    using (var fileStream = new FileStream(_texturePath, FileMode.Open))
                        _texture = Texture2D.FromStream(GameInstance.TheGame.GraphicsDevice, fileStream);
                return _texture;
            }
        }
    }
}
