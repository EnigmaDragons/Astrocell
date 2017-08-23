using System.Collections.Generic;
using MonoDragons.Core.IO;

namespace Astrocell.Battles.Decks
{
    public struct Card
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ActionPointCost { get; set; }
        public int EnergyCost { get; set; }

        public int EnergyGain { get; set; }
        public int CardsDrawn { get; set; }

        public List<CardEffect> Effects { get; set; }

        public static Card Load(string name)
        {
            return new JsonIo().Load<Card>($"./Content/Cards/{name}.json");
        }
    }

    public struct CardEffect
    {
        public EffectTarget Target { get; set; }
        public EffectType Type { get; set; }
        public EffectStat Stat { get; set; }
        public StatusEffect Status { get; set; }
        public int Duration { get; set; }
        public float Factor { get; set; }
    }

    public enum EffectStat
    {
        None,
        Magic,
        Attack,
        Toughness,
    }

    public enum EffectTarget
    {
        One,
        AllEnemies,
        AllAllies,
        None,
        Self
    }

    public enum EffectType
    {
        None,
        Damage,
        Heal,
        Status
    }

    public enum StatusEffect
    {
        None,
        Buff,
        Stun,
    }
}
