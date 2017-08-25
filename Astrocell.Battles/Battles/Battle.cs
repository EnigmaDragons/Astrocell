using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Players;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public sealed class Battle
    {
        private readonly ILog _log;
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
            return new Battle(BattleLog.Instance, gamer, enemy, characters.OrderByDescending(x => x.Initiative).ToList());
        }

        public static Battle Create(ILog log, IPlayer gamer, IPlayer enemy, params BattleCharacter[] characters)
        {
            return new Battle(log, gamer, enemy, characters.OrderByDescending(x => x.Initiative).ToList());
        }

        private Battle(ILog log, IPlayer gamer, IPlayer enemy, IList<BattleCharacter> characters)
        {
            _log = log;
            _players = new Dictionary<BattleSide, IPlayer> {{ BattleSide.Gamer, gamer}, { BattleSide.Enemy, enemy} };
            TurnOrder = new LoopingSequence<BattleCharacter>(characters.ToList());
        }

        public BattleSide Resolve()
        {
            _log.Write($"Began Battle with {TurnOrder.Items.CommaSeparated(x => x.Name)}.");

            while (!IsOver)
                ResolveNextTurn();

            var winner = PlayerWon ? BattleSide.Gamer : BattleSide.Enemy;

            _log.Write($"Winner: {winner}");
            return winner;
        }

        private void ResolveNextTurn()
        {
            var chr = TurnOrder.Next();
            _log.Write($"Began turn for {chr.Loyalty} {chr.Name}.");

            if (chr.CanAct)
                chr.BeginTurn();
            while (chr.CanPlayACard)
                ResolveAction(chr);

            chr.EndTurn();
        }

        private void ResolveAction(BattleCharacter chr)
        {
            var player = _players[chr.Loyalty];
            var action = player.SelectAction(chr, chr.PlayableCards, new BattleCharacters(TurnOrder.Items));
            action.Apply(this);
        }
    }
}
