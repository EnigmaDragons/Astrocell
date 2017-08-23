using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public struct PhysicalDamageEffect : IBattleEffect
    {
        private readonly int _amount;

        public PhysicalDamageEffect(int amount)
        {
            _amount = amount;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.TakePhysicalDamage(_amount);
        }
    }
}
