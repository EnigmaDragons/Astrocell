using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class FirstValidCardPlayer : IPlayer
    {
        public CardAction SelectAction(BattleCharacter forCharacter, IList<Card> cards, IList<BattleCharacter> allCharacters)
        {
            var card = cards[0];
            var targets = SelectTargets(forCharacter, card.Effect.Target, allCharacters);
            return new CardAction { Source = forCharacter, Card = card, Targets = targets };
        }

        private List<BattleCharacter> SelectTargets(BattleCharacter src, EffectTarget target, IList<BattleCharacter> allCharacters)
        {
            if (target == EffectTarget.Self)
                return src.AsList();
            if (target == EffectTarget.AllEnemies)
                return allCharacters.Where(x => x.Loyalty == BattleSide.Enemy).ToList();
            if (target == EffectTarget.AllAllies)
                return allCharacters.Where(x => x.Loyalty == BattleSide.Gamer).ToList();
            // TODO: Make this selection smarter
            if (target == EffectTarget.One)
                return allCharacters.First(x => x.Loyalty == BattleSide.Enemy).AsList();
            return new List<BattleCharacter>();
        }
    }
}
