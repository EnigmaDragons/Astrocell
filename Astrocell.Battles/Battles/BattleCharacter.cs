using System.Linq;
using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleCharacter
    {
        private readonly ICharStats _stats;

        public int Initiative => _stats.Agility;
        public bool IsConscious => CurrentHp > 0;
        public bool CanAct => IsConscious;
        public bool CanPlayACard => CanAct && Hand.Cards.Any(x => x.ActionPointCost <= CurrentActionPoints && x.EnergyCost <= CurrentEnergy);

        public int MaxHp => _stats.MaxHp;
        public BattleSide Loyalty { get; }
        public BattleDeck Deck { get; }
        public BattleHand Hand { get; }
        public int CurrentHp { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentActionPoints { get; set; }
        public string Name { get; set; }

        public static BattleCharacter Init(BattleSide side, CharacterSheet charSheet)
        {
            return new BattleCharacter(charSheet.Stats, side, BattleDeck.Create(charSheet.Deck.Cards));
        }

        private BattleCharacter(ICharStats stats, BattleSide loyalty, BattleDeck deck)
        {
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
            Hand.Take(card);
            CurrentEnergy -= card.EnergyCost;
            CurrentActionPoints -= card.ActionPointCost;

            DrawCards(card.CardsDrawn);
            CurrentEnergy += card.EnergyGain;
        }

        public void ChangeHp(int amount)
        {
            CurrentHp += amount;
            if (CurrentHp > MaxHp)
                CurrentHp = MaxHp;
            if (CurrentHp < 0)
                CurrentHp = 0;
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

        private void DrawCards(int n)
        {
            n.PerformNTimes(() => Hand.Add(Deck.Draw()));
        }
    }
}
