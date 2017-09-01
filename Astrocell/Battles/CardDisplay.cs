using System.Collections.Generic;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Text;

namespace Astrocell.Battles
{
    public static class CardDisplay
    {
        public static GameObject Create(Card card)
        {
            return Entity.Create(new Transform2(new Size2(200, 300)))
                .Add(new MouseDrag())
                .Add((o, r) => new Texture(r.CreateRectangle(Color.Coral, o)))
                .Add(new MultiTextDisplay { Displays = new List<TextDisplay> {
                        new TextDisplay {Align = TextAlign.TopCenter, Text = () => card.Name },
                        new TextDisplay {Align = TextAlign.TopLeft, Text = () => $"{card.ActionPointCost} AP"},
                        new TextDisplay {Align = TextAlign.TopRight, Text = () => card.EnergyCost > 0 ? $"{card.EnergyCost} E" : ""},
                        new TextDisplay {Align = TextAlign.Center, Text = () => card.Description},
                } });
        }
    }
}
