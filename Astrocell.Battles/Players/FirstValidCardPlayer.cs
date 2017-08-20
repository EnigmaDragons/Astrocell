using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Players
{
    public sealed class FirstValidCardPlayer : IPlayer
    {
        public CardAction SelectAction(BattleCharacter forCharacter, IList<Card> cards, BattleCharacters allCharacters)
        {
            var card = cards[0];
            var targets = SelectTargets(forCharacter, card.Effect.Target, allCharacters);
            return new CardAction { Source = forCharacter, Card = card, Targets = targets };
        }

        private IList<BattleCharacter> SelectTargets(BattleCharacter src, EffectTarget target, BattleCharacters allCharacters)
        {
            var possibleTargets = allCharacters.GetPossibleTargets(src, target);
            // TODO: Make this selection smarter
            if (target == EffectTarget.One)
                return possibleTargets.First(x => x.Loyalty != src.Loyalty).AsList();
            return possibleTargets;
        }
    }
}
