using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.IO;

namespace MonoDragons.Core.Entities
{
    public sealed class EntityResources
    {
        private readonly string _basePath;
        private readonly Dictionary<string, HashSet<long>> _assetHandles = new Dictionary<string, HashSet<long>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, IDisposable> _assets = new Dictionary<string, IDisposable>(StringComparer.OrdinalIgnoreCase);

        public int Count => _assets.Count;

        public EntityResources(string basePath = "./Content")
        {
            _basePath = basePath;
        }

        public Texture2D CreateRectangle(Color color, GameObject obj)
        {
            var asset = $"{color}RectangleTexture";
            UpdateAssetHandles(obj, asset);
            if (!_assets.ContainsKey(asset))
                _assets[asset] = new RectangleTexture(color).Create();
            return (Texture2D)_assets[asset];
        }

        public Texture2D LoadTexture(string filePath, GameObject obj)
        {
            var asset = Normalize(filePath);
            UpdateAssetHandles(obj, asset);
            if (!_assets.ContainsKey(asset))
                _assets[asset] = new FileTexture2D(asset).Load();
            return (Texture2D)_assets[asset];
        }

        public void Release(GameObject obj)
        {
            _assetHandles.ForEach(x => x.Remove(obj.Id));
            CleanupUnusedResource();
        }

        private void CleanupUnusedResource()
        {
            _assetHandles.KeysWhere(x => x.Value.Count == 0)
                .ForEach(x =>
                {
                    _assetHandles.Remove(x);
                    _assets[x].Dispose();
                    _assets.Remove(x);
                });
        }

        private void UpdateAssetHandles(GameObject obj, string asset)
        {
            if (!_assetHandles.ContainsKey(asset))
                _assetHandles[asset] = new HashSet<long>();
            _assetHandles[asset].Add(obj.Id);
        }

        private string Normalize(string filePath)
        {
            return Path.Combine(_basePath, filePath).Replace("\\", "/").ToLowerInvariant();
        }
    }
}
