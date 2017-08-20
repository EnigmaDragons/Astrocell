using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Effects;

namespace Astrocell.Battles.Decks
{
    public struct CardAction
    {
        public BattleCharacter Source { get; set; }
        public Card Card { get; set; }
        public IList<BattleCharacter> Targets { get; set; }

        public void Apply(Battle battle)
        {
            Source.Play(Card);
            var effect = BattleEffect.Create(Source, Card.Effect);
            Targets.ForEach(x => effect.ApplyTo(x));
        }
    }
}
