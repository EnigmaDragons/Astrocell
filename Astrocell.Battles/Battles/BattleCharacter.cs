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
        public bool CanPlayACard => CanAct && Hand.Cards.Any(x => x.ActionPointCost <= CurrentActionPoints && x.EnergyCost <= CurrentEnergy);

        public string Name { get; }
        public BattleSide Loyalty { get; }
        public BattleDeck Deck { get; }
        public BattleHand Hand { get; }

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

            DrawCards(card.CardsDrawn);
            CurrentEnergy += card.EnergyGain;
        }

        // TODO: This design for applying effects can't be right.
        public void TakePhysicalDamage(int amount)
        {
            var dmgAmount = amount - _stats.Defense;
            ChangeHp(-dmgAmount);

            _log.Write($"{Name} suffers {dmgAmount} physical damage.");
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
            return 0;
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
