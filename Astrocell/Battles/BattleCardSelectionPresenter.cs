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
        private readonly BattleTargetSelection _targetting;
        private readonly List<GameObject> _currentObjs = new List<GameObject>();

        public BattleCardSelectionPresenter(Action<GameObject> registerObj, BattleTargetSelection targetting)
        {
            _registerObj = x => { registerObj(x); _currentObjs.Add(x); };
            _targetting = targetting;
        }
        
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
            _targetting.OnCardSelected(src, chars, selectedCard, onCardSelected);
        }
    }
}
