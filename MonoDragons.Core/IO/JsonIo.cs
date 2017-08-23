using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonoDragons.Core.IO
{
    public sealed class JsonIo
    {
        public T Load<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }

        public void Save(string filePath, object data)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(data, Formatting.Indented, new StringEnumConverter()));
        }
    }
}
