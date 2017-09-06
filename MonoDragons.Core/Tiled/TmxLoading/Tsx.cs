using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct Tsx
    {
        public int TileWidth;
        public int TileHeight;
        public int TileCount;
        public int Spacing;
        public int Columns;
        public string ImageSource;
        public int MsPerFrame;
        public List<TsxSprite> Sprites;

        public static Tsx Create(string tsxPath)
        {
            var doc = XDocument.Load(Path.Combine("Content", tsxPath));
            var spriteSheet = doc.Element(XName.Get("tileset"));
            return new Tsx
            {
                TileWidth = new XValue(spriteSheet, "tilewidth").AsInt(),
                TileHeight = new XValue(spriteSheet, "tileheight").AsInt(),
                TileCount = new XValue(spriteSheet, "tilecount").AsInt(),
                Spacing = new XValue(spriteSheet, "spacing").AsInt(),
                Columns = new XValue(spriteSheet, "columns").AsInt(),
                ImageSource = GetSourcePath(spriteSheet.Element(XName.Get("image")), tsxPath),
                MsPerFrame = new XProperty(spriteSheet, "FrameLength").AsInt(),
                Sprites = spriteSheet.Elements("tile").Select(x => TsxSprite.Create(x)).ToList(),
            };
        }

        private static string GetSourcePath(XElement image, string tsxPath)
        {
            var imageSource = new XValue(image, "source").AsString();
            var tsxDirectory = Path.GetDirectoryName(tsxPath);
            return Path.Combine(tsxDirectory, imageSource);
        }
    }
}
