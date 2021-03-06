﻿using System.Collections.Generic;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Text;
using Astrocell.Plugins;
using MonoDragons.Core.Common;

namespace Astrocell.Battles
{
    public static class CardDisplay
    {
        private static int _zIndex = 100;

        public const int Width = 200;

        public static GameObject Create(Card card, Vector2 position, bool isPlayable)
        {
            _zIndex += 3;

            var obj = Entity.Create($"Card: {card.Name}" ,new Transform2 { Location = position, Size = new Size2(Width, 300), ZIndex = new ZIndex(_zIndex) })
                .Add(new CardDataComponent { Card = card })
                .Add(new BorderTexture())
                .Add((o, r) => new Texture(r.CreateRectangle(Color.Coral, o)))
                .Add(Entity.Create($"CardText", new Transform2 { Location = position, Size = new Size2(Width, 300), ZIndex = new ZIndex(_zIndex) })
                .Add(new MultiTextDisplay
                {
                    Displays = new List<TextDisplay> {
                        new TextDisplay {Align = TextAlign.TopCenter, Text = () => card.Name },
                        new TextDisplay {Align = TextAlign.TopLeft, Text = () => $"{card.ActionPointCost} AP"},
                        new TextDisplay {Align = TextAlign.TopRight, Text = () => card.EnergyCost > 0 ? $"{card.EnergyCost} E" : ""},
                        new TextDisplay {Align = TextAlign.Center, Text = () => card.Description},
                    }
                }));

            isPlayable.If(() =>
                obj.Add(new HighlightColor { Offset = -2, Width = 12, CornerRadius = 5, Color = Color.Transparent })
                    .Add(o => new MouseStateActions
                    {
                        OnHover = () => o.With<HighlightColor>(h => h.Color = Color.Red),
                        OnExit = () => o.With<HighlightColor>(h => h.Color = Color.Transparent)
                    })
                    .Add(new MouseDragAndDrop()));

            return obj;
        }
    }
}
