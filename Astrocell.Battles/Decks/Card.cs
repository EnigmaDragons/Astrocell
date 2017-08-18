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

        public CardEffect Effect { get; set; }

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
        public float Factor { get; set; }
    }

    public enum EffectStat
    {
        None,
        Magic,
        Attack
    }

    public enum EffectTarget
    {
        One,
        Many,
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
}
