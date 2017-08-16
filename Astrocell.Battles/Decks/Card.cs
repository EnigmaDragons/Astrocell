namespace Astrocell.Battles.Decks
{
    public struct Card
    {
        public int ActionPointCost { get; set; }
        public int EnergyCost { get; set; }
        public int EnergyGain { get; set; }
        public int CardsDrawn { get; set; }
        public float PhysicalDamage { get; set; }
        public float MagicalDamage { get; set; }
        public ActionTarget TargetType { get; set; }

        public static Card Load(string name)
        {
            return new JsonIo().Load<Card>($"./Content/Cards/{name}.json");
        }
    }
}
