using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.KeyboardControls
{
    public class TopDownController : ISystem
    {
        private readonly Map<Keys, HorizontalDirection> _hDirBind;
        private readonly Map<Keys, VerticalDirection> _vDirBind;

        public TopDownController() : this(CreateDefaultHDirBind(), CreateDefaultVDirBind()) { }

        public TopDownController(Map<Keys, HorizontalDirection> hDirBind, Map<Keys, VerticalDirection> vDirBind)
        {
            _hDirBind = hDirBind;
            _vDirBind = vDirBind;
        }

        public void Update(IEntities entities, TimeSpan delta)
        {
            var downKeys = Keyboard.GetState().GetPressedKeys().ToList();
            var direction = GetDirection(downKeys);
            entities.With<TopDownMovement>(movement =>
                movement.GameObject.With<Motion2>(
                    motion => motion.Velocity = new Velocity2
                    {
                        Speed = direction.HDir != 0 || direction.VDir != 0 ? movement.Speed : 0,
                        Direction = direction.ToRotation()
                    }));
        }

        private Direction GetDirection(List<Keys> downKeys)
        {
            var hDir = (HorizontalDirection)downKeys
                .Where(x => _hDirBind.ContainsKey(x))
                .Select(x => (int)_hDirBind[x])
                .Distinct()
                .Sum();
            var vDir = (VerticalDirection)downKeys
                .Where(x => _vDirBind.ContainsKey(x))
                .Select(x => (int)_vDirBind[x])
                .Distinct()
                .Sum();
            return new Direction(hDir, vDir);
        }

        private static Map<Keys, VerticalDirection> CreateDefaultVDirBind()
        {
            return new Map<Keys, VerticalDirection>
            {
                {Keys.W, VerticalDirection.Up},
                {Keys.S, VerticalDirection.Down},
                {Keys.Up, VerticalDirection.Up},
                {Keys.Down, VerticalDirection.Down},
            };
        }

        private static Map<Keys, HorizontalDirection> CreateDefaultHDirBind()
        {
            return new Map<Keys, HorizontalDirection>
            {
                {Keys.A, HorizontalDirection.Left},
                {Keys.D, HorizontalDirection.Right},
                {Keys.Left, HorizontalDirection.Left},
                {Keys.Right, HorizontalDirection.Right},
            };
        }
    }
}
