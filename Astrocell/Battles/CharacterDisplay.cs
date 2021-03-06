﻿using Astrocell.Battles.Battles;
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
        public static GameObject Create(BattleCharacter character, string charImg, Vector2 location, BattleTargetSelection targets)
        {
            string CharText(string x) => $"Char {character.Name}: {x}";
            return Entity.Create(CharText("Background"), new Transform2 { Location = location, Size = new Size2(300, 400) })
                .Add((o, r) => new Texture(r.LoadTexture("Battle/char-portrait-back.png", o)))
                .Add(Entity.Create(CharText("Image"), new Transform2 { Location = location + new Vector2(30, 80), Size = new Size2(240, 240), ZIndex = 1 })
                    .Add((o, r) => new Texture(r.LoadTexture(charImg, o))))
                .Add(Entity.Create(CharText("Hp Bar"), new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 40), ZIndex = 1 })
                    .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-back.png", o)))
                    .Add(new TextDisplay { Text = () => $"{character.CurrentHp}/{character.MaxHp}" }))
                .Add(Entity.Create(CharText("Hp Bar"), new Transform2 { Location = location + new Vector2(30, 30), Size = new Size2(240, 40), ZIndex = 2 })
                    .Add((o, r) => new Texture(r.LoadTexture("Battle/hp-bar-filled.png", o)))
                    .Add(new PercentBar { CurrentPercent = () => character.CurrentHp / (float)character.MaxHp, MaxWidth = 240 }))
                .Add(Entity.Create(CharText("Current Action Points"), new Transform2 { Location = location + new Vector2(40, 310), Size = new Size2(60, 60), ZIndex = 3 })
                    .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                    .Add(new TextDisplay { Text = () => $"{character.CurrentActionPoints}AP" }))
                .Add(Entity.Create(CharText("Current Energy"), new Transform2 { Location = location + new Vector2(215, 310), Size = new Size2(60, 60), ZIndex = 3 })
                    .Add((o, r) => new Texture(r.LoadTexture("Battle/blue-icon.png", o)))
                    .Add(new TextDisplay { Text = () => $"{character.CurrentEnergy}NG" }))
                .Add(Entity.Create(CharText("Mouse Click Zone"), new Transform2 { Location = location, Size = new Size2(300, 400), ZIndex = 4 })
                    .Add(new MouseClickTarget { OnHit = () => targets.OnCharacterIndicated(character) }));
        }
    }
}
