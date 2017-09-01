using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public struct MagicDamageEffect : IBattleEffect
    {
        private readonly int _amount;

        public MagicDamageEffect(int amount)
        {
            _amount = amount;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.TakeMagicDamage(_amount);
        }
    }
}
