using System.IO;
using System.Xml.Linq;

namespace MonoDragons.Core.Tiled.TmxLoading
{
    public class TmxExists
    {
        private readonly string _path;

        public TmxExists(string path)
        {
            _path = path;
        }

        public bool Get()
        {
            try
            {
                XDocument.Load(Path.Combine("Content", _path));
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
