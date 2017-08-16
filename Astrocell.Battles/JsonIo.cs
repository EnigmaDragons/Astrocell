using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Astrocell.Battles
{
    public sealed class JsonIo
    {
        static JsonIo()
        {
            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            });
        }

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
