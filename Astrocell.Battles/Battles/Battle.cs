using System.Collections.Generic;
using System.Linq;

namespace Astrocell.Battles.Battles
{
    public sealed class Battle
    {
        private readonly IList<BattleCharacter> _characters;

        public LoopingSequence<BattleCharacter> TurnOrder { get; }

        private bool EnemyWon => SideIsAllUnconscious(BattleSide.Player);
        private bool PlayerWon => SideIsAllUnconscious(BattleSide.Enemy);
        private bool IsOver => EnemyWon || PlayerWon;

        private bool SideIsAllUnconscious(BattleSide side)
        {
            return TurnOrder.Items.Where(x => x.Loyalty.Equals(side)).All(x => !x.IsConscious);
        }

        public static Battle Create(params BattleCharacter[] characters)
        {
            return new Battle(characters.OrderByDescending(x => x.Initiative).ToList());
        }

        private Battle(IList<BattleCharacter> characters)
        {
            _characters = characters;
            TurnOrder = new LoopingSequence<BattleCharacter>(_characters.ToList());
        }

        public BattleSide Resolve()
        {
            while (!IsOver)
                ResolveNextTurn();
            return PlayerWon
                ? BattleSide.Player
                : BattleSide.Enemy;
        }

        private void ResolveNextTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}
