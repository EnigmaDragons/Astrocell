using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Players
{
    public sealed class AiPlayer : IPlayer
    {
        public CardAction SelectAction(BattleCharacter forCharacter, IList<Card> cards, BattleCharacters allCharacters)
        {
            var card = SelectCard(forCharacter, cards, allCharacters);
            var targets = card.Effects.Any()
                ? SelectTargets(forCharacter, card.Effects[0].Target, allCharacters)
                : new List<BattleCharacter>();
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

        private Card SelectCard(BattleCharacter forCharacter, IList<Card> cards, BattleCharacters allCharacters)
        {
            foreach (var card in cards)
            {
                if (card.Effects.Count > 0 && card.Effects[0].Type == EffectType.Heal && forCharacter.MissingHpPercent > 0.5)
                    continue;
                return card;
            }
            return cards[0];
        }
    }
}
