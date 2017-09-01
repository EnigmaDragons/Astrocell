using System.Collections.Generic;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Effects;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Decks
{
    public struct CardAction
    {
        public BattleCharacter Source { get; set; }
        public Card Card { get; set; }
        public IList<TargettedEffect> TargettedEffects { get; set; }

        public void Apply(Battle battle)
        {
            var src = Source;
            src.Play(Card);
            TargettedEffects.ForEach(x => x.Effect.ApplyTo(src, x.Targets));
        }
    }

    public struct TargettedEffect
    {
        public CardEffect Effect { get; set; }
        public IList<BattleCharacter> Targets { get; set; }

        public TargettedEffect(CardEffect effect, IList<BattleCharacter> targets)
        {
            Effect = effect;
            Targets = targets;
        }
    }
}
