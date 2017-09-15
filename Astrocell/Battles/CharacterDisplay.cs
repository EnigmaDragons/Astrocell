using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using Astrocell.Plugins;

namespace Astrocell.Battles
{
    public static class CharacterDisplay
    {
        public static List<GameObject> Create(BattleCharacter character, string charImg, Vector2 location)
        {
            var display = Entity.Create(new Transform2 {Location = location, Size = new Size2(300, 400)})
                .Add((o, r) => new Texture(r.LoadTexture("Battle/char-portrait-back.png", o)))
                .Add(new MouseDrag());
            var face = Entity.Create(new Transform2 {Location = location + new Vector2(30, 100), Size = new Size2(240, 240), ZIndex = 8 })
                .Add((o, r) => new Texture(r.LoadTexture(charImg, o)))
                .AttachTo(display);

            var hpBarBack = Entity.Create(new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 60), ZIndex = 9 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-back.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentHp}/{character.MaxHp}" })
                .AttachTo(display);
            var hpBar = Entity.Create(new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 60), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-filled.png", o)))
                .Add(new PercentBar { CurrentPercent = () => character.CurrentHp / (float)character.MaxHp, MaxWidth = 240 })
                .AttachTo(display);

            var apIcon = Entity.Create(new Transform2 { Location = location + new Vector2(25, 300), Size = new Size2(75, 75), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentActionPoints} AP" })
                .AttachTo(display);

            var energyIcon = Entity.Create(new Transform2 { Location = location + new Vector2(200, 300), Size = new Size2(75, 75), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentEnergy} NG" })
                .AttachTo(display);

            return new List<GameObject> { display, face, hpBarBack, hpBar, apIcon, energyIcon };
        }
    }
}
