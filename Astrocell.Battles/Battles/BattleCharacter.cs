using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Effects;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleCharacter
    {
        private readonly ILog _log;
        private readonly BattleCharacterStats _stats;
        private readonly BattleCharacterStatusEffects _effects;

        public int Initiative => _stats[BattleStat.Initiative];
        public bool IsConscious => _stats[BattleStat.CurrentHp] > 0;
        public bool CanAct => IsConscious && _effects.CanAct;
        public bool CanPlayACard => CanAct && Hand.Cards.Any(CanAfford);

        public string Name { get; }
        public BattleSide Loyalty { get; }
        public BattleDeck Deck { get; }
        public BattleHand Hand { get; }
        public IList<Card> PlayableCards => Hand.Cards.Where(CanAfford).ToList();

        public int CurrentHp => _stats.CurrentHp;
        public int CurrentEnergy => _stats.CurrentEnergy;
        public int CurrentActionPoints => _stats.CurrentActionPoints;
        public float MissingHpPercent => _stats[BattleStat.MaxHp] - CurrentHp / (float)_stats[BattleStat.MaxHp];

        public static BattleCharacter Create(BattleSide side, CharacterSheet charSheet)
        {
            return new BattleCharacter(BattleLog.Instance, charSheet.Name, charSheet.Stats, side, BattleDeck.Create(charSheet.Deck.Cards));
        }

        private BattleCharacter(ILog log, string name, ICharStats stats, BattleSide loyalty, BattleDeck deck)
        {
            _log = log;
            Name = name;
            Hand = new BattleHand();
            _stats = new BattleCharacterStats(stats);
            _effects = new BattleCharacterStatusEffects();
            Loyalty = loyalty;
            Deck = deck;
            DrawCards(_stats[BattleStat.StartingCards]);
        }

        public void BeginTurn()
        {
            _stats.CurrentActionPoints = _stats[BattleStat.ActionPoints];
            DrawCards(_stats[BattleStat.Draw]);
        }

        public void Play(Card card)
        {
            _log.Write($"{Name} plays {card.Name}.");

            Hand.Take(card);
            _stats.CurrentEnergy -= card.EnergyCost;
            _stats.CurrentActionPoints -= card.ActionPointCost;

            if (card.CardsDrawn > 0)
                _log.Write($"{Name} draws {card.CardsDrawn} Cards.");
            DrawCards(card.CardsDrawn);

            if (card.EnergyGain > 0)
                _log.Write($"{Name} gains {card.EnergyGain} Energy.");
            _stats.CurrentEnergy += card.EnergyGain;
        }
        
        public void TakePhysicalDamage(int amount)
        {
            var dmgAmount = amount - _stats[BattleStat.Defense];
            _stats.ChangeHp(-dmgAmount);
            _log.Write($"{Name} suffers {dmgAmount} physical damage.");
        }

        public void TakeMagicDamage(int amount)
        {
            var dmgAmount = amount - _stats[BattleStat.Resistance];
            _stats.ChangeHp(-dmgAmount);
            _log.Write($"{Name} suffers {dmgAmount} magic damage.");
        }

        public void Heal(int amount)
        {
            _stats.ChangeHp(amount);
            _log.Write($"{Name} heals {amount} HP.");
        }

        public void ApplyBuff(BattleStat stat, float factor, int duration)
        {
            if (stat == BattleStat.None)
                return;

            _stats.ApplyBuff(stat, factor, duration);
            _log.Write($"{Name} has {stat} increased by {factor:0.0}x for {duration} turns.");
        }

        public void ApplyStatusEffect(StatusEffect effect, int duration)
        {
            if (effect == StatusEffect.None)
                return;

            _effects.Apply(effect, duration);
            _log.Write($"{Name} is {effect} for {duration} turns.");
        }

        public void EndTurn()
        {
            _stats.EndTurn();
            _effects.EndTurn();
        }

        public int GetStat(BattleStat stat)
        {
            return _stats[stat];
        }

        private bool CanAfford(Card x)
        {
            return x.ActionPointCost <= _stats[BattleStat.CurrentActionPoints] && x.EnergyCost <= _stats[BattleStat.CurrentEnergy];
        }

        private void DrawCards(int n)
        {
            n.PerformNTimes(() => Hand.Add(Deck.Draw()));
        }
    }
}
