using System;
using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.BattlePresentation;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Players;
using MonoDragons.Core.Common;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public sealed class Battle
    {
        private enum Phase
        {
            NotStarted,
            AwaitingAction,
            Presenting,
            AwaitingTurn,
            TurnFinished,
            BattleFinished
        }

        // TODO: Replace Logging with Battle Presentation
        private readonly ILog _log;
        private readonly IBattlePresenter _presenter;
        private readonly Dictionary<BattleSide, IPlayer> _players;

        public LoopingSequence<BattleCharacter> TurnOrder { get; }
        public bool IsOver => EnemyWon || PlayerWon;

        private BattleCharacter CurrentChar => TurnOrder.Current;
        private bool EnemyWon => SideIsAllUnconscious(BattleSide.Gamer);
        private bool PlayerWon => SideIsAllUnconscious(BattleSide.Enemy);
        private Phase State { get; set; }

        private bool SideIsAllUnconscious(BattleSide side)
        {
            return TurnOrder.Items.Where(x => x.Loyalty.Equals(side)).All(x => !x.IsConscious);
        }

        public static Battle Create(IPlayer gamer, IPlayer enemy, params BattleCharacter[] characters)
        {
            return new Battle(BattleLog.Instance, BattlePresenter.Instance, gamer, enemy, characters.OrderByDescending(x => x.Initiative).ToList());
        }

        private Battle(ILog log, IBattlePresenter presenter, IPlayer gamer, IPlayer enemy, IList<BattleCharacter> characters)
        {
            _presenter = presenter;
            _log = log;
            _players = new Dictionary<BattleSide, IPlayer> {{ BattleSide.Gamer, gamer}, { BattleSide.Enemy, enemy} };
            TurnOrder = new LoopingSequence<BattleCharacter>(characters.ToList());
        }

        public void Advance()
        {
            if (State == Phase.Presenting)
                return;

            if (IsOver)
                FinishBattle();
            else if (State == Phase.NotStarted)
                BeginBattle();
            else if (State == Phase.AwaitingTurn)
                BeginNextTurn();
            else if (State == Phase.AwaitingAction)
                BeginNextAction();
            else if (State == Phase.TurnFinished)
                EndTurn();
        }

        private void EndTurn()
        {
            TurnOrder.Current.EndTurn();
            _log.Write("");
            State = Phase.AwaitingTurn;
        }

        private void FinishBattle()
        {
            var winner = PlayerWon ? BattleSide.Gamer : BattleSide.Enemy;
            _log.Write($"Winner: {winner}");
            State = Phase.BattleFinished;
        }

        private void BeginBattle()
        {
            _log.Write("");
            _log.Write($"Began Battle with {TurnOrder.Items.CommaSeparated(x => x.Name)}.");
            State = Phase.AwaitingTurn;
        }

        public BattleSide Resolve()
        {
            while (State != Phase.BattleFinished)
                Advance();
            return PlayerWon ? BattleSide.Gamer : BattleSide.Enemy;
        }

        private void BeginNextTurn()
        {
            TurnOrder.Next();
            _log.Write($"Began turn for {CurrentChar.Loyalty} {CurrentChar.Name}.");

            if (CurrentChar.CanAct)
            {
                State = Phase.AwaitingAction;
                CurrentChar.BeginTurn();
            }
            else
                State = Phase.TurnFinished;
        }

        private void BeginNextAction()
        {
            if (CurrentChar.CanPlayACard)
                BeginAction();
            else
                State = Phase.TurnFinished;
        }
        
        private void BeginAction()
        {
            var chr = CurrentChar;
            var player = _players[chr.Loyalty];
            var action = player.SelectAction(chr, chr.PlayableCards, new BattleCharacters(TurnOrder.Items));
            State = Phase.Presenting;
            _presenter.ShowSelectedCard(action.Card, ResolveCard(action));
        }

        private Action ResolveCard(CardAction action)
        {
            return () =>
            {
                action.Apply(this);
                State = CurrentChar.CanPlayACard ? Phase.AwaitingAction : Phase.TurnFinished;
            };
        }
    }
}
