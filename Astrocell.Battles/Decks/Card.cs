using System.Collections.Generic;
using Astrocell.Battles.Battles;
using MonoDragons.Core.IO;

namespace Astrocell.Battles.Decks
{
    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; } = "Missing description";

        public int ActionPointCost { get; set; }
        public int EnergyCost { get; set; }

        public int EnergyGain { get; set; }
        public int CardsDrawn { get; set; }

        public List<CardEffect> Effects { get; set; } = new List<CardEffect>();

        public static Card Load(string name)
        {
            return new JsonIo().Load<Card>($"./Content/Cards/{name}.json");
        }
    }

    public struct CardEffect
    {
        public EffectTarget Target { get; set; }
        public EffectType Type { get; set; }
        public BattleStat Stat { get; set; }
        public CardStatusEffect Status { get; set; }
        public int Duration { get; set; }
        public float Factor { get; set; }
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
        Status,
        Buff,
    }

    public enum CardStatusEffect
    {
        None,
        Stun,
    }
}
