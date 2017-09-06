using System;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public class XValue
    {
        private readonly XElement _element;
        private readonly string _key;

        public XValue(XElement element, string key)
        {
            _element = element;
            _key = key;
        }

        public int AsInt()
        {
            return int.Parse(_element.Attribute(XName.Get(_key))?.Value ?? "0");
        }

        public string AsString()
        {
            return _element.Attribute(XName.Get(_key)).Value;
        }

        public bool AsBool()
        {
            return Convert.ToBoolean(AsString());
        }
    }
}
