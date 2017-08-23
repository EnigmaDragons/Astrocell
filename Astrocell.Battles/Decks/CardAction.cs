using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Effects;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Decks
{
    public struct CardAction
    {
        public BattleCharacter Source { get; set; }
        public Card Card { get; set; }
        // TODO: Replace this with Targetted Effects to handle multiple effects better
        public IList<BattleCharacter> Targets { get; set; }

        public void Apply(Battle battle)
        {
            var src = Source;
            var targets = Targets;
            src.Play(Card);
            var effects = Card.Effects.Select(x => BattleEffect.Create(src, x));
            effects.ForEach(x => targets.ForEach(x.ApplyTo));
        }
    }
}
