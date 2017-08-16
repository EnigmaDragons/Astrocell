namespace Astrocell.Battles.Characters
{
    public sealed class CharacterSheet
    {
        public ICharStats Stats { get; }
        public EquipmentSheet Equipment { get; }

        public CharacterSheet(ICharStats stats, EquipmentSheet equip)
        {
            Stats = stats;
            Equipment = equip;
        }
    }
}
