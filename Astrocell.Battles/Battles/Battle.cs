using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Players;

namespace Astrocell.Battles.Battles
{
    public sealed class Battle
    {
        private readonly Dictionary<BattleSide, IPlayer> _players;

        public LoopingSequence<BattleCharacter> TurnOrder { get; }

        private bool EnemyWon => SideIsAllUnconscious(BattleSide.Gamer);
        private bool PlayerWon => SideIsAllUnconscious(BattleSide.Enemy);
        private bool IsOver => EnemyWon || PlayerWon;

        private bool SideIsAllUnconscious(BattleSide side)
        {
            return TurnOrder.Items.Where(x => x.Loyalty.Equals(side)).All(x => !x.IsConscious);
        }

        public static Battle Create(IPlayer gamer, IPlayer enemy, params BattleCharacter[] characters)
        {
            return new Battle(gamer, enemy, characters.OrderByDescending(x => x.Initiative).ToList());
        }

        private Battle(IPlayer gamer, IPlayer enemy, IList<BattleCharacter> characters)
        {
            _players = new Dictionary<BattleSide, IPlayer> {{ BattleSide.Gamer, gamer}, { BattleSide.Enemy, enemy} };
            TurnOrder = new LoopingSequence<BattleCharacter>(characters.ToList());
        }

        public BattleSide Resolve()
        {
            while (!IsOver)
                ResolveNextTurn();
            return PlayerWon
                ? BattleSide.Gamer
                : BattleSide.Enemy;
        }

        private void ResolveNextTurn()
        {
            var chr = TurnOrder.Next();

            if (chr.CanAct)
                chr.BeginTurn();
            while (chr.CanPlayACard)
                ResolveAction(chr);

            chr.EndTurn();
        }

        private void ResolveAction(BattleCharacter chr)
        {
            var player = _players[chr.Loyalty];
            var action = player.SelectAction(chr.Hand.Cards, TurnOrder.Items);
            action.Apply(this);
        }
    }
}
