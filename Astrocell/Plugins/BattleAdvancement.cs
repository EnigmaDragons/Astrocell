using System;
using Astrocell.Battles.Battles;
using MonoDragons.Core.Entities;

namespace Astrocell.Plugins
{
    public sealed class CurrentBattle : EntityComponent
    {
        public Battle Battle { get; set; }
    }

    public sealed class BattleAdvancement : ISystem
    {
        public void Update(IEntities entities, TimeSpan delta)
        {
            entities.With<CurrentBattle>(x =>
            {
                if (!x.Battle.IsOver)
                    x.Battle.Advance();
            });
        }
    }
}
