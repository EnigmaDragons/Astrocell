﻿using System;
using System.Linq;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.PhysicsEngine
{
    public sealed class ZGravitation : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            var min = new MinHeight();
            entities.Collect<ZGravity>()
                .OrderBy(x => x.World.ZIndex.Value)
                    .ForEach(x => Update(min, x));
        }

        private static void Update(MinHeight min, GameObject obj)
        {
            var minZ = min.Value;
            var currentZ = obj.World.ZIndex;
            var grav = obj.Get<ZGravity>();
            if (grav.IsEnabled && currentZ.Value > minZ)
                obj.Local.ZIndex = new ZIndex(Math.Max(minZ, currentZ.Value - grav.Acceleration) - obj.World.ZIndex.Value);
            min.Value = obj.World.ZIndex.Value + 1;
        }

        private class MinHeight
        {
            public int Value { get; set; }
        }
    }
}
