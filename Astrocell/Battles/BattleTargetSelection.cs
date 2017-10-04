using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrocell.Battles
{
    public sealed class BattleTargetSelection
    {
        private Optional<Card> SelectedCard { get; set; } = Optional<Card>.Missing;
        private BattleCharacters Chars { get; set; }
        private BattleCharacter Source { get; set; }
        private Action<CardAction> Callback { get; set; }

        private List<TargettedEffect> ChosenEffects { get; set; } = new List<TargettedEffect>();
        private int _effectIndex;

        private CardEffect CurrentEffect => SelectedCard.Value.Effects[_effectIndex];

        public void OnCharacterIndicated(BattleCharacter character)
        {
            if (!SelectedCard.IsPresent)
                return;

            ChosenEffects.Add(new TargettedEffect(CurrentEffect, character.AsList()));

            while (++_effectIndex < SelectedCard.Value.Effects.Count && RequiresNoManualTargetting(CurrentEffect))
                ChosenEffects.Add(SelectInferredTargets(Source, Chars, CurrentEffect));

            if (_effectIndex < SelectedCard.Value.Effects.Count)
                return;

            var card = SelectedCard.Value;
            SelectedCard = Optional<Card>.Missing;
            Callback(new CardAction { Card = card, Source = Source, TargettedEffects = ChosenEffects });
        }

        public void OnCardSelected(BattleCharacter src, BattleCharacters chars, Card card, Action<CardAction> onActionSelected)
        {
            if (card.Effects.None() || card.Effects.TrueForAll(RequiresNoManualTargetting))
            {
                var effects = card.Effects.Select(x => SelectInferredTargets(src, chars, x)).ToList();
                onActionSelected(new CardAction { Card = card, Source = src, TargettedEffects = effects });
                return;
            }
            
            Callback = onActionSelected;
            SelectedCard = card;
            Source = src;
            _effectIndex = 0;
            ChosenEffects.Clear();
        }

        private bool RequiredManualTargetting(CardEffect effect)
        {
            return effect.Target == EffectTarget.One;
        }

        private bool RequiresNoManualTargetting(CardEffect effect)
        {
            return effect.Target != EffectTarget.One;
        }

        private TargettedEffect SelectInferredTargets(BattleCharacter src, BattleCharacters chars, CardEffect effect)
        {
            return new TargettedEffect(effect, chars.GetPossibleTargets(src, effect.Target));
        }
    }
}
