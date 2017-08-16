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

        public int MaxHp => _stats.MaxHp;
        public BattleSide Loyalty { get; }
        public BattleDeck Deck { get; }
        public BattleHand Hand { get; }
        public int CurrentHp { get; set; }
        public int CurrentEnergy { get; set; }

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
            DrawStartingHand();
        }

        private void DrawStartingHand()
        {
            _stats.StartingCards.PerformNTimes(DrawCard);
        }

        public void DrawForTurn()
        {
            _stats.Draw.PerformNTimes(DrawCard);
        }

        private void DrawCard()
        {
            Hand.Add(Deck.Draw());
        }
    }
}
