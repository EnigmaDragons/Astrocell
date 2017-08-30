using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleCharacter
    {
        private readonly ILog _log;
        private readonly ICharStats _stats;

        public int Initiative => _stats.Agility;
        public bool IsConscious => CurrentHp > 0;
        public bool CanAct => IsConscious;
        public bool CanPlayACard => CanAct && Hand.Cards.Any(CanAfford);

        public string Name { get; }
        public BattleSide Loyalty { get; }
        public BattleDeck Deck { get; }
        public BattleHand Hand { get; }
        public IList<Card> PlayableCards => Hand.Cards.Where(CanAfford).ToList();

        public int MaxHp => _stats.MaxHp;
        public int CurrentHp { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentActionPoints { get; set; }

        public static BattleCharacter Init(BattleSide side, CharacterSheet charSheet)
        {
            return new BattleCharacter(BattleLog.Instance, charSheet.Name, charSheet.Stats, side, BattleDeck.Create(charSheet.Deck.Cards));
        }

        private BattleCharacter(ILog log, string name, ICharStats stats, BattleSide loyalty, BattleDeck deck)
        {
            _log = log;
            Name = name;
            Hand = new BattleHand();
            _stats = stats;
            Loyalty = loyalty;
            Deck = deck;
            CurrentHp = MaxHp;
            CurrentEnergy = _stats.StartingEnergy;
            DrawCards(_stats.StartingCards);
        }

        public void BeginTurn()
        {
            CurrentActionPoints = _stats.ActionPoints;
            DrawCards(_stats.Draw);
        }

        public void Play(Card card)
        {
            _log.Write($"{Name} plays {card.Name}.");

            Hand.Take(card);
            CurrentEnergy -= card.EnergyCost;
            CurrentActionPoints -= card.ActionPointCost;

            if (card.CardsDrawn > 0)
                _log.Write($"{Name} draws {card.CardsDrawn} Cards.");
            DrawCards(card.CardsDrawn);

            if (card.EnergyGain > 0)
                _log.Write($"{Name} gains {card.EnergyGain} Energy.");
            CurrentEnergy += card.EnergyGain;
        }

        // TODO: This design for applying effects can't be right.
        public void TakePhysicalDamage(int amount)
        {
            var dmgAmount = amount - _stats.Defense;
            ChangeHp(-dmgAmount);
            _log.Write($"{Name} suffers {dmgAmount} physical damage.");
        }

        public void TakeMagicDamage(int amount)
        {
            var dmgAmount = amount - _stats.Resistance;
            ChangeHp(-dmgAmount);
            _log.Write($"{Name} suffers {dmgAmount} magic damage.");
        }

        public void Heal(int amount)
        {
            ChangeHp(amount);
            _log.Write($"{Name} heals {amount} HP.");
        }

        public void EndTurn()
        {
        }

        public int GetStat(EffectStat stat)
        {
            if (stat == EffectStat.Attack)
                return _stats.Attack;
            if (stat == EffectStat.Magic)
                return _stats.Magic;
            if (stat == EffectStat.Toughness)
                return _stats.Toughness;
            throw new KeyNotFoundException($"Unknown EffectStat {stat}");
        }

        private bool CanAfford(Card x)
        {
            return x.ActionPointCost <= CurrentActionPoints && x.EnergyCost <= CurrentEnergy;
        }

        private void ChangeHp(int amount)
        {
            CurrentHp += amount;
            if (CurrentHp > MaxHp)
                CurrentHp = MaxHp;
            if (CurrentHp < 0)
                CurrentHp = 0;
        }

        private void DrawCards(int n)
        {
            n.PerformNTimes(() => Hand.Add(Deck.Draw()));
        }
    }
}
