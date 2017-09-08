using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Plugins;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;

namespace Astrocell.Battles
{
    public sealed class BattleScene : EcsScene
    {
        private const int BackgroundLayer = 0;
        private const int CombatLogLayer = 1;

        protected override IEnumerable<GameObject> CreateObjs()
        {
            var log = new BufferedLog();
            yield return Entity.Create(new Transform2 {Location = new Vector2(0, -100), Size = new Size2(1600, 1228), ZIndex = BackgroundLayer})
                .Add((o, r) => new Texture(r.LoadTexture("Battle/tek-orange-room.jpg", o)));
            yield return Entity.Create(new Transform2 {Location = new Vector2(150, 50), Size = new Size2(1300, 50), ZIndex = CombatLogLayer})
                .Add((o, r) => new Texture(r.CreateRectangle(Color.DarkBlue, o)))
                .Add((o, r) => new BorderTexture(r.CreateRectangle(Color.AntiqueWhite, o)))
                .Add(log)
                .Add(new TextDisplay {Text = () => log.Lines.Last()});
            yield return CharacterDisplay.Create(
                BattleCharacter.Create(BattleSide.Gamer, Samples.CreateElectrician()),
                "Sprites/gareth-face.png",
                new Vector2(1200, 450));
            new BattleSimulator(log).Resolve1V1(Samples.CreateDumbBrute(), Samples.CreateDumbBrute());
        }
    }
}
