using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;

namespace Astrocell.Battles.Players
{
    public sealed class AIPlayer : IPlayer
    {
        public CardAction SelectAction(BattleCharacter src, IList<Card> cards, BattleCharacters allCharacters)
        {
            var card = SelectCard(src, cards, allCharacters);
            var targettedEffects = card.Effects.Select(x => SelectTargets(x, src, allCharacters)).ToList();
            return new CardAction { Source = src, Card = card, TargettedEffects = targettedEffects };
        }

        private TargettedEffect SelectTargets(CardEffect effect, BattleCharacter src, BattleCharacters allCharacters)
        {
            var targetType = effect.Target;
            var possibleTargets = allCharacters.GetPossibleTargets(src, targetType);
            // TODO: Make this selection smarter
            if (targetType == EffectTarget.One)
                return new TargettedEffect(effect, possibleTargets.First(x => x.Loyalty != src.Loyalty).AsList());
            return new TargettedEffect(effect, possibleTargets);
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
