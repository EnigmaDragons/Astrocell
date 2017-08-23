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
                .OrderBy(x => x.Transform.ZIndex)
                    .ForEach(x => Update(min, x));
        }

        private static void Update(MinHeight min, GameObject obj)
        {
            var minZ = min.Value;
            var currentZ = obj.Transform.ZIndex;
            var grav = obj.Get<ZGravity>();
            if (grav.IsEnabled && currentZ > minZ)
                obj.Transform.ZIndex = Math.Max(minZ, currentZ - grav.Acceleration);
            min.Value = obj.Transform.ZIndex + 1;
        }

        private class MinHeight
        {
            public int Value { get; set; }
        }
    }
}
