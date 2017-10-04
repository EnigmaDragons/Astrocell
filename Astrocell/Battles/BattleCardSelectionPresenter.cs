using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Players;
using Astrocell.Plugins;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.MouseControls;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrocell.Battles
{
    public sealed class BattleCardSelectionPresenter : IPlayer
    {
        private readonly Action<GameObject> _registerObj;
        private readonly List<GameObject> _currentObjs = new List<GameObject>();

        public BattleCardSelectionPresenter(Action<GameObject> registerObj)
        {
            _registerObj = x => { registerObj(x); _currentObjs.Add(x); } ;
        }

        public object CardComponent { get; private set; }

        public void SelectAction(BattleCharacter src, BattleHand hand, BattleCharacters chars, Action<CardAction> onCardSelected)
        {
            _registerObj(Entity.Create("Player Card Select DropZone", new Transform2 { Size = new Size2(1920, 500) })
                .Add(new MouseDropTarget { OnDrop = x => CompleteCardSelection(src, chars, x, onCardSelected) }));

            var margin = 20;
            var xLoc = 100;
            hand.Cards.ForEachIndex((x, i) => _registerObj(
                CardDisplay.Create(x, new Vector2(xLoc + i * (CardDisplay.Width + margin), 600), hand.Playable.Contains(x))));
        }
        
        private void CompleteCardSelection(BattleCharacter src, BattleCharacters chars, GameObject obj, Action<CardAction> onCardSelected)
        {
            var selectedCard = obj.Get<CardDataComponent>().Card;
            _currentObjs.DequeueEach(x => Entity.Destroy(x));
            CompleteTargetSelection(src, selectedCard, chars, onCardSelected);
        }

        // TODO: Allow the player to select their own targets
        private void CompleteTargetSelection(BattleCharacter src, Card card, BattleCharacters characters, Action<CardAction> onCardSelected)
        {
            var targets = card.Effects.Select(x => SelectTargets(x, src, characters)).ToList();
            onCardSelected(new CardAction { Card = card, Source = src, TargettedEffects = targets });
        }

        private TargettedEffect SelectTargets(CardEffect effect, BattleCharacter src, BattleCharacters allCharacters)
        {
            var targetType = effect.Target;
            var possibleTargets = allCharacters.GetPossibleTargets(src, targetType);
            if (targetType == EffectTarget.One)
                return new TargettedEffect(effect, possibleTargets.First(x => x.Loyalty != src.Loyalty).AsList());
            return new TargettedEffect(effect, possibleTargets);
        }
    }
}
