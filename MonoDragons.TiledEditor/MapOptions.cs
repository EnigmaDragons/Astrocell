using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoDragons.Core.Common;

namespace MonoDragons.TiledEditor
{
    public class MapOptions
    {
        private readonly Action<string> _actOnMapPath;

        public MapOptions(Action<string> actOnMapPath)
        {
            _actOnMapPath = actOnMapPath;
        }

        public IEnumerable<Option> Get()
        {
            return Directory.GetFiles(Path.Combine("Content", "Maps"))
                .Where(fileName => Path.GetExtension(fileName).ToLower() == ".tmx")
                .Select(mapName => new Option(Path.GetFileName(mapName), () => _actOnMapPath(GetRelativePathUpOneFolder(mapName))));
        }

        private string GetRelativePathUpOneFolder(string path)
        {
            var newPath = path.Substring(path.IndexOf('\\') + 1);
            return newPath;
        }
    }
}
