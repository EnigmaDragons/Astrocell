using System;
using System.Collections.Generic;
using Astrocell.Battles.BattlePresentation;
using Astrocell.Battles.Decks;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Timing;

namespace Astrocell.Battles
{
    public sealed class UIBattlePresenter : EntityComponent, IBattlePresenter
    {
        private readonly List<TimerAction> _presentations = new List<TimerAction>();

        public void Update(TimeSpan delta)
        {
            if (_presentations.None())
                return;

            _presentations.ForEach(x => x.Update(delta));
            _presentations.RemoveAll(x => x.IsDone);
        }

        public void ShowSelectedCard(Card card, Action continueWith)
        {
            var cardEntity = ShowCard(card);

            _presentations.Add(new TimerAction
            {
                TimerMode = TimerAction.Mode.Once,
                Action = () => { HideCard(cardEntity); continueWith(); },
                Interval = TimeSpan.FromMilliseconds(3000)
            });
        }

        private List<GameObject> ShowCard(Card card)
        {
            return CardDisplay.Create(card)
                .Add(o => new DurationTravel
                {
                    Duration = TimeSpan.FromMilliseconds(1000),
                    Target = new Transform2 {Size = o.Local.Size, Location = new Vector2(700, 350)} 
                }).AsList();
        }

        private void HideCard(List<GameObject> objs)
        {
            objs.ForEach(Entity.Destroy);
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
