using System;
using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.BattlePresentation;
using Astrocell.Battles.Battles;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.Logs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Timing;

namespace Astrocell.Battles
{
    public sealed class UIBattlePresenter : EntityComponent, IBattlePresenter
    {
        private readonly List<TimerAction> _presentations = new List<TimerAction>();
        private readonly ILog _log;
        private readonly Action<GameObject> _registerObj;

        public UIBattlePresenter(ILog log, Action<GameObject> registerObj)
        {
            _log = log;
            _registerObj = registerObj;
        }

        public void Update(TimeSpan delta)
        {
            if (_presentations.None())
                return;

            _presentations.ForEach(x => x.Update(delta));
            _presentations.RemoveAll(x => x.IsDone);
        }

        private List<GameObject> ShowCard(Card card)
        {
            var objs = CardDisplay.Create(card);
            objs.First().Add(o => new DurationTravel
            {
                Duration = TimeSpan.FromMilliseconds(1000),
                Target = new Transform2 {Size = o.Local.Size, Location = new Vector2(700, 350), ZIndex = o.World.ZIndex}
            });
            objs.ForEach(x => _registerObj(x));
            return objs;
        }

        private void HideCard(List<GameObject> objs)
        {
            objs.ForEach(Entity.Destroy);
        }

        public void ShowBattleBegan(Battle battle, Action callback)
        {
            _log.Write($"Began Battle with {battle.Characters.Snapshot.CommaSeparated(x => x.Name)}.");
            callback();
        }

        public void ShowTurnBegan(BattleCharacter character, Action callback)
        {
            _log.Write($"Began turn for {character.Loyalty} {character.Name}.");
            callback();
        }

        public void ShowPlayedCard(BattleCharacter character, Card card, Action callback)
        {
            var cardEntity = ShowCard(card);

            _presentations.Add(new TimerAction
            {
                TimerMode = TimerAction.Mode.Once,
                Action = () => { HideCard(cardEntity); callback(); },
                Interval = TimeSpan.FromMilliseconds(3000)
            });
        }

        public void ShowTurnEnded(BattleCharacter character, Action callback)
        {
            _log.Write($"Ended turn for {character.Loyalty} {character.Name}.");
            callback();
        }

        public void ShowBattleEnded(Battle battle, Action callback)
        {
            _log.Write($"Winner: {battle.Winner}");
            callback();
        }
    }

    public sealed class UpdateBattlePresenter : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<UIBattlePresenter>(x => x.Update(delta));
        }
    }
}
