using System;
using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.BattlePresentation;
using Astrocell.Battles.Decks;
using Astrocell.Battles.Players;

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
            AwaitingPlayerSelection,
            TurnFinished,
            BattleFinished
        }
        
        private readonly IBattlePresenter _presenter;
        private readonly Dictionary<BattleSide, IPlayer> _players;

        public LoopingSequence<BattleCharacter> Characters { get; }
        public BattleSide Winner => IsOver ? (PlayerWon ? BattleSide.Gamer : BattleSide.Enemy) : BattleSide.Neutral;
        public bool HasFinished => State == Phase.BattleFinished;

        private BattleCharacter CurrentChar => Characters.Current;
        private bool EnemyWon => SideIsAllUnconscious(BattleSide.Gamer);
        private bool PlayerWon => SideIsAllUnconscious(BattleSide.Enemy);
        private bool IsOver => EnemyWon || PlayerWon;
        private Phase State { get; set; }

        private bool SideIsAllUnconscious(BattleSide side)
        {
            return Characters.Snapshot.Where(x => x.Loyalty.Equals(side)).All(x => !x.IsConscious);
        }

        public static Battle Create(IPlayer gamer, IPlayer enemy, params BattleCharacter[] characters)
        {
            return new Battle(BattlePresenter.Instance, gamer, enemy, characters.OrderByDescending(x => x.Initiative).ToList());
        }

        private Battle(IBattlePresenter presenter, IPlayer gamer, IPlayer enemy, IList<BattleCharacter> characters)
        {
            _presenter = presenter;
            _players = new Dictionary<BattleSide, IPlayer> {{ BattleSide.Gamer, gamer}, { BattleSide.Enemy, enemy} };
            Characters = new LoopingSequence<BattleCharacter>(characters.ToList());
        }

        public BattleSide Resolve()
        {
            while (State != Phase.BattleFinished)
                Advance();
            return Winner;
        }

        public void Advance()
        {
            if (State == Phase.BattleFinished)
                return;
            else if (IsOver)
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

        private void BeginBattle()
        {
            Present(x => x.ShowBattleBegan(this, () => State = Phase.AwaitingTurn));
        }

        private void FinishBattle()
        {
            Present(x => x.ShowBattleEnded(this, () => State = Phase.BattleFinished));
        }

        private void BeginNextTurn()
        {
            Characters.Next();
            CurrentChar.BeginTurn();

            Present(x => x.ShowTurnBegan(CurrentChar, () => State = Phase.AwaitingAction));
        }

        private void EndTurn()
        {
            CurrentChar.EndTurn();
            Present(x => x.ShowTurnEnded(CurrentChar, () => State = Phase.AwaitingTurn));
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
            State = Phase.AwaitingPlayerSelection;
            player.SelectAction(chr, chr.Hand, new BattleCharacters(Characters.Snapshot), 
                action => Present(x => x.ShowPlayedCard(chr, action.Card, ResolveCard(action))));
        }

        private Action ResolveCard(CardAction action)
        {
            return () =>
            {
                action.Apply(this);
                State = Phase.AwaitingAction;
            };
        }

        private void Present(Action<IBattlePresenter> present)
        {
            State = Phase.Presenting;
            present(_presenter);
        }
    }
}
