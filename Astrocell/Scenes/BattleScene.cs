using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles;
using Astrocell.Battles.Battles;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Logs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;

namespace Astrocell.Scenes
{
    public sealed class BattleScene : EcsScene
    {
        private const int BackgroundLayer = 0;
        private const int CombatLogLayer = 1;

        protected override IEnumerable<GameObject> CreateObjs()
        {
            var log = new InMemoryLog();
            BattleLog.Instance = log;
            yield return Entity.Create(new Transform2 {Location = new Vector2(0, -100), Size = new Size2(1600, 1228), ZIndex = BackgroundLayer})
                .Add((o, r) => new Texture(r.LoadTexture("Battle/tek-orange-room.jpg", o)));
            yield return Entity.Create(new Transform2 {Location = new Vector2(150, 50), Size = new Size2(1300, 50), ZIndex = CombatLogLayer})
                .Add((o, r) => new Texture(r.CreateRectangle(Color.DarkBlue, o)))
                .Add((o, r) => new BorderTexture(r.CreateRectangle(Color.AntiqueWhite, o)))
                .Add(new TextDisplay {Text = () => log.Lines.Last()});
            new BattleSimulator(log).Resolve1V1(Samples.CreateDumbBrute(), Samples.CreateDumbBrute());
        }
    }
}
