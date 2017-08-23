using System;
using System.IO;

namespace MonoDragons.Core.IO
{
    public sealed class AppDataJsonIo
    {
        private readonly string _gameStorageFolder;
        private readonly JsonIo _io = new JsonIo();

        public AppDataJsonIo(string gameFolderName)
        {
            _gameStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameFolderName);
        }

        public T Load<T>(string saveName)
        {
            return _io.Load<T>(GetSavePath(saveName));
        }

        public void Save(string saveName, object data)
        {
            _io.Save(GetSavePath(saveName), data);
        }

        private string GetSavePath(string saveName)
        {
            return Path.Combine(_gameStorageFolder, saveName) + ".sav";
        }
    }
}
