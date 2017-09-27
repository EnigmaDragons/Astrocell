using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Plugins;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Scenes;
using Astrocell.Battles.Players;
using System;
using Astrocell.Battles.BattlePresentation;
using Astrocell.Battles.Enemies;

namespace Astrocell.Battles
{
    public sealed class BattleScene : EcsScene
    {
        private const int BackgroundLayer = 0;
        private const int CombatLogLayer = 3;

        protected override IEnumerable<GameObject> CreateObjs()
        {
            var delay = TimeSpan.FromMilliseconds(800);
            var log = new BufferedLog { BufferDuration = delay };
            var presenter = new UIBattlePresenter(log);
            yield return Entity.Create("Battle UI Presenter")
                .Add(presenter);
            yield return Entity.Create("Battle Background", new Transform2 { Location = new Vector2(0, -100), Size = new Size2(1600, 1228), ZIndex = BackgroundLayer })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/tek-orange-room.jpg", o)));
            yield return Entity.Create("Battle Log", new Transform2 { Location = new Vector2(150, 50), Size = new Size2(1300, 50), ZIndex = CombatLogLayer })
                .Add((o, r) => new Texture(r.CreateRectangle(Color.DarkBlue, o)))
                .Add((o, r) => new BorderTexture(r.CreateRectangle(Color.AntiqueWhite, o)))
                .Add(log)
                .Add(new TextDisplay { Text = () => log.Lines.Last() });
            var char1Battle = BattleCharacter.Create(BattleSide.Gamer, Samples.CreateElectrician());
            var char1 = CharacterDisplay.Create(
                char1Battle,
                "Heroes/gareth.png",
                new Vector2(1200, 450));
            foreach(var obj in char1)
                yield return obj;

            var enemy1Battle = BattleCharacter.Create(BattleSide.Enemy, Enemy.CreateLaserDrone());
            var enemy1 = CharacterDisplay.Create(enemy1Battle, "Enemies/drone1.png", new Vector2(200, 450));
            foreach (var obj in enemy1)
                yield return obj;

            BattlePresenter.Instance = presenter;
            BattleLog.Instance = log;
            var battle = Battle.Create(new AIPlayer(), new AIPlayer(), char1Battle, enemy1Battle);
            yield return Entity.Create("Current Battle")
                .Add(new CurrentBattle {Battle = battle});
        }
    }
}
