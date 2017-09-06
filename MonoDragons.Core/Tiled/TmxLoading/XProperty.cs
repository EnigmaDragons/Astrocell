using System;
using System.Linq;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public class XProperty
    {
        private readonly XElement _element;
        private readonly string _propertyName;

        public XProperty(XElement element, string propertyName)
        {
            _element = element;
            _propertyName = propertyName;
        }

        public int AsInt()
        {
            return int.Parse(AsString());
        }

        public string AsString()
        {
            return new XValue(
                _element.Element(XName.Get("properties"))
                    .Elements(XName.Get("property"))
                        .First(x => new XValue(x, "name").AsString() == _propertyName), 
                "value").AsString();
        }

        public bool AsBool()
        {
            return Convert.ToBoolean(AsString());
        }
    }
}
