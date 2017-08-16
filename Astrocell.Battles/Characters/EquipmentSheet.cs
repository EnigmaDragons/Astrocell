namespace Astrocell.Battles.Characters
{
    public sealed class EquipmentSheet
    {
        public static EquipmentSheet Empty = new EquipmentSheet();

        public ICharExtrinsicStats GetStatMods()
        {
            return new ExtrinsicStatsMods();
        }
    }
}
