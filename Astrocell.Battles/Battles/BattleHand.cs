using System.Collections.Generic;
using Astrocell.Battles.Decks;
using System;
using System.Linq;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleHand
    {
        private readonly Func<int> _getCurrentAp;
        private readonly Func<int> _getCurrentEnergy;
        private readonly List<Card> _cards = new List<Card>();

        public IEnumerable<Card> Cards => _cards;
        public IEnumerable<Card> Playable => _cards.Where(CanAfford);

        public bool CanAfford(Card card) => _getCurrentAp() >= card.ActionPointCost && _getCurrentEnergy() >= card.EnergyCost;

        public BattleHand(Func<int> getCurrentAp, Func<int> getCurrentEnergy)
        {
            _getCurrentAp = getCurrentAp;
            _getCurrentEnergy = getCurrentEnergy;
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public Card Take(Card card)
        {
            _cards.Remove(card);
            return card;
        }
    }
}
