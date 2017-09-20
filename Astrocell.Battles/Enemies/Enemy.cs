using Astrocell.Battles.Characters;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Enemies
{
    public static class Enemy
    {
        public static CharacterSheet CreateLaserDrone(string name = "Laser Drone")
        {
            return new CharacterBuilder()
                .WithName(name)
                .WithDeck(EquippedDeckFactory.LoadEnemy("laserdrone"))
                .WithStats(new EnemyStartingStats
                {
                    Level = 1,
                    Strength = 8,
                    Agility = 6,
                    Intelligence = 6,
                    Willpower = 6,
                    Toughness = 4
                })
                .WithEquipment(EquipmentSheet.Empty)
                .Build();
        }
    }
}
