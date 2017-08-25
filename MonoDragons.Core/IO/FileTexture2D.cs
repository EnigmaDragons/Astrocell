using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.IO
{
    public sealed class FileTexture2D
    {
        private readonly string _filePath;

        public FileTexture2D(string filePath)
        {
            _filePath = filePath;
        }

        public Texture2D Load()
        {
            using (var file = File.OpenRead(_filePath))
                return Texture2D.FromStream(GameInstance.GraphicsDevice, file);
        }
    }
}
