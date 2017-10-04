using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;

namespace Astrocell.Battles
{
    public sealed class BattleTargetSelection
    {
        private BattleCharacters Chars { get; set; }
        private BattleCharacter Source { get; set; }
        private Optional<Card> SelectedCard { get; set; } = Optional<Card>.Missing;
        private Action<CardAction> Callback { get; set; }

        private List<TargettedEffect> ChosenEffects { get; set; } = new List<TargettedEffect>();
        private int _effectIndex;

        public void OnCharacterIndicated(BattleCharacter character)
        {
            if (!SelectedCard.IsPresent)
                return;

            ChosenEffects.Add(new TargettedEffect(SelectedCard.Value.Effects[_effectIndex], character.AsList()));
            if (++_effectIndex < SelectedCard.Value.Effects.Count)
                return;

            var card = SelectedCard.Value;
            SelectedCard = Optional<Card>.Missing;
            Callback(new CardAction { Card = card, Source = Source, TargettedEffects = ChosenEffects });
        }

        public void OnCardSelected(BattleCharacter src, BattleCharacters chars, Card card, Action<CardAction> onActionSelected)
        {
            if (card.Effects.None())
            {
                onActionSelected(new CardAction { Card = card, Source = src, TargettedEffects = new List<TargettedEffect>() });
                return;
            }

            Callback = onActionSelected;
            SelectedCard = card;
            Source = src;
            _effectIndex = 0;
            ChosenEffects.Clear();
        }
    }
}
