using System;
using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Characters;
using Astrocell.Battles.Effects;
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
        private readonly Store<IBattleCharacterStats> _stats = new Store<IBattleCharacterStats>();
        private readonly IList<DurationBattleCharacterStatsMod> _mods = new List<DurationBattleCharacterStatsMod>();

        public int this[BattleStat stat] => GetStat(stat);

        public int Initiative => this[BattleStat.Agility];
        public int CurrentHp { get; set; }
        public int CurrentEnergy { get; set; }
        public int CurrentActionPoints { get; set; }

        public BattleCharacterStats(ICharStats stats)
        {
            _stats.Put(new CharStatsAsBattleStats(stats));
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

        public void ApplyBuff(BattleStat stat, float factor, int duration)
        {
            var mod = new DurationBattleCharacterStatsMod(_stats.Get(), stat, factor, duration);
            _mods.Add(mod);
            _stats.Put(mod);
        }

        public void EndTurn()
        {
            _mods.ForEach(x => x.EndTurn());
        }

        private int GetStat(BattleStat stat)
        {
            var statName = stat.ToString();
            return statName.MatchesOneOf<Extrinsic>() || statName.MatchesOneOf<Intrinsic>()
                ? _stats.Get()[stat]
                : this.GetPropertyValue<int>(statName).Value;
        }

        private interface IBattleCharacterStats
        {
            int this[BattleStat stat] { get; }
        }

        private struct CharStatsAsBattleStats : IBattleCharacterStats
        {
            private readonly ICharStats _stats;

            public int this[BattleStat stat] => GetStat(stat.ToString());

            public CharStatsAsBattleStats(ICharStats stats)
            {
                _stats = stats;
            }

            private int GetStat(string statName)
            {
                if (Enum.GetNames(typeof(Intrinsic)).Any(x => x.Equals(statName)))
                    return _stats[(Intrinsic)Enum.Parse(typeof(Intrinsic), statName)];
                if (Enum.GetNames(typeof(Extrinsic)).Any(x => x.Equals(statName)))
                    return _stats[(Extrinsic)Enum.Parse(typeof(Extrinsic), statName)];
                return this.GetPropertyValue<int>(statName).Value;
            }
        }

        private sealed class DurationBattleCharacterStatsMod : IBattleCharacterStats
        {
            private readonly IBattleCharacterStats _base;
            private readonly BattleStat _stat;
            private readonly float _factor;
            private int _remainingTurnsActive;

            public int this[BattleStat stat] => _base[stat].StatMultiplyBy(FactorFor(stat));

            public DurationBattleCharacterStatsMod(IBattleCharacterStats baseStats, BattleStat stat, float factor, int duration)
            {
                _base = baseStats;
                _stat = stat;
                _factor = factor;
                _remainingTurnsActive = duration;
            }

            private float FactorFor(BattleStat stat)
            {
                return _remainingTurnsActive > 0 && stat == _stat
                    ? _factor
                    : 1.0f;
            }

            public void EndTurn()
            {
                _remainingTurnsActive = Math.Max(0, _remainingTurnsActive - 1);
            }
        }
    }
}
