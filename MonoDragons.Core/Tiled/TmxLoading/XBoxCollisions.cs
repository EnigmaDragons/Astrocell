using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public class XBoxCollisions
    {
        private readonly XElement _element;

        public XBoxCollisions(XElement element)
        {
            _element = element;
        }

        public List<Rectangle> Get()
        {
            return _element.Element(XName.Get("objectgroup"))
                .Elements(XName.Get("object"))
                .Select(x => new Rectangle(
                    new XValue(x, "x").AsInt(),
                    new XValue(x, "y").AsInt(),
                    new XValue(x, "width").AsInt(),
                    new XValue(x, "height").AsInt())).ToList();
        }
    }
}
