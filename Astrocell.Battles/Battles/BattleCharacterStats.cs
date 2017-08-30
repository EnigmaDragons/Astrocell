using System;
using System.Linq;
using Astrocell.Battles.Characters;
using MonoDragons.Core.Common;
using MonoDragons.Core.Common.Reflection;

namespace Astrocell.Battles.Battles
{
    public enum BattleStat
    {
        None,
        Level,
        Strength,
        Agility,
        Toughness,
        Willpower,
        Intelligence,
        CurrentHp,
        CurrentEnergy,
        CurrentActionPoints,
        MaxHp,
        Attack,
        Magic,
        Resistance,
        Defense,
        Draw,
        ActionPoints,
        StartingEnergy,
        StartingCards,
        EnergyGain,
        Initiative,
    }

    public sealed class BattleCharacterStats
    {
        public int this[BattleStat stat] => GetStat(stat.ToString());

        private readonly ICharStats _base;
        private readonly Store<ICharStats> _stats = new Store<ICharStats>();

        public int Initiative => this[BattleStat.Agility];
        public int CurrentHp { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentActionPoints { get; set; }

        public BattleCharacterStats(ICharStats stats)
        {
            _base = stats;
            _stats.Put(stats);
            CurrentHp = this[BattleStat.MaxHp];
            CurrentEnergy = this[BattleStat.StartingEnergy];
        }
        
        public void ChangeHp(int amount)
        {
            CurrentHp += amount;
            if (CurrentHp > this[BattleStat.MaxHp])
                CurrentHp = this[BattleStat.MaxHp];
            if (CurrentHp < 0)
                CurrentHp = 0;
        }

        private int GetStat(string statName)
        {
            if (Enum.GetNames(typeof(Intrinsic)).Any(x => x.Equals(statName)))
                return _stats.Get()[(Intrinsic)Enum.Parse(typeof(Intrinsic), statName)];
            if (Enum.GetNames(typeof(Extrinsic)).Any(x => x.Equals(statName)))
                return _stats.Get()[(Extrinsic)Enum.Parse(typeof(Extrinsic), statName)];
            return this.GetPropertyValue<int>(statName).Value;
        }
    }
}
