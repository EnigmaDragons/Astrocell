using System.Collections.Generic;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Text;
using Astrocell.Plugins;

namespace Astrocell.Battles
{
    public static class CardDisplay
    {
        private static int _zIndex = 100;

        public const int Width = 200;

        public static GameObject Create(Card card, Vector2 position)
        {
            _zIndex += 3;
            return Entity.Create($"Card: {card.Name}" ,new Transform2 { Location = position, Size = new Size2(Width, 300), ZIndex = new ZIndex(_zIndex) })
                .Add(new HighlightColor { Offset = -2, Width = 8, CornerRadius = 3 })
                .Add(new CardDataComponent { Card = card })
                .Add(new MouseDragAndDrop())
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
        }
    }
}
