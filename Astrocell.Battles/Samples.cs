using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles
{
    public static class Samples
    {
        public static CharacterSheet CreateElectrician(string name = "Electrician")
        {
            return new CharacterBuilder()
                .WithName(name)
                .WithDeck(EquippedDeckFactory.BuildElectricianDeck())
                .WithStats(new StartingStats
                {
                    Level = 1,
                    Strength = 3,
                    Agility = 6,
                    Intelligence = 12,
                    Willpower = 12,
                    Toughness = 6
                })
                .WithEquipment(EquipmentSheet.Empty)
                .Build();
        }

        public static CharacterSheet CreateDumbBrute(string name = "Dumb Brute")
        {
            return new CharacterBuilder()
                 .WithName(name)
                 .WithDeck(EquippedDeckFactory.BuildDumbBruteDeck())
                 .WithStats(new StartingStats
                 {
                     Level = 1,
                     Strength = 15,
                     Agility = 6,
                     Intelligence = 6,
                     Willpower = 3,
                     Toughness = 9
                 })
                 .WithEquipment(EquipmentSheet.Empty)
                 .Build();
        }
    }
}
