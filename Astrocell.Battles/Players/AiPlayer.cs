using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;
using System;

namespace Astrocell.Battles.Players
{
    public sealed class AIPlayer : IPlayer
    {
        public void SelectAction(BattleCharacter src, BattleHand hand, BattleCharacters allCharacters, Action<CardAction> onCardSelected)
        {
            var card = SelectCard(src, hand.Playable, allCharacters);
            var targettedEffects = card.Effects.Select(x => SelectTargets(x, src, allCharacters)).ToList();
            onCardSelected(new CardAction { Source = src, Card = card, TargettedEffects = targettedEffects });
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

        private Card SelectCard(BattleCharacter forCharacter, IEnumerable<Card> cards, BattleCharacters allCharacters)
        {
            foreach (var card in cards)
            {
                if (card.Effects.Count > 0 && card.Effects[0].Type == EffectType.Heal && forCharacter.MissingHpPercent > 0.5)
                    continue;
                return card;
            }
            return cards.First();
        }
    }
}
