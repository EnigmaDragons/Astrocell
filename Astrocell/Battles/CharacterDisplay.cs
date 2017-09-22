using System;
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
            string CharText(string x) => $"Char {character.Name}: {x}";
            var display = Entity.Create(CharText("Background"), new Transform2 {Location = location, Size = new Size2(300, 400)})
                .Add((o, r) => new Texture(r.LoadTexture("Battle/char-portrait-back.png", o)))
                .Add(new MouseDrag());
            var face = Entity.Create(CharText("Image"), new Transform2 {Location = location + new Vector2(30, 80), Size = new Size2(240, 240), ZIndex = 8 })
                .Add((o, r) => new Texture(r.LoadTexture(charImg, o)))
                .AttachTo(display);

            var hpBarBack = Entity.Create(CharText("Hp Bar"), new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 40), ZIndex = 9 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-back.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentHp}/{character.MaxHp}" })
                .AttachTo(display);
            var hpBar = Entity.Create(CharText("Hp Bar"), new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 40), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-filled.png", o)))
                .Add(new PercentBar { CurrentPercent = () => character.CurrentHp / (float)character.MaxHp, MaxWidth = 240 })
                .AttachTo(display);

            var apIcon = Entity.Create(CharText("Current Action Points"), new Transform2 { Location = location + new Vector2(40, 310), Size = new Size2(60, 60), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentActionPoints}AP" })
                .AttachTo(display);

            var energyIcon = Entity.Create(CharText("Current Energy"), new Transform2 { Location = location + new Vector2(215, 310), Size = new Size2(60, 60), ZIndex = 10 })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                .Add(new TextDisplay { Text = () => $"{character.CurrentEnergy}NG" })
                .AttachTo(display);

            return new List<GameObject> { display, face, hpBarBack, hpBar, apIcon, energyIcon };
        }
    }
}
