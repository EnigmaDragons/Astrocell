using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public struct DamageEffect : IBattleEffect
    {
        private readonly int _amount;

        public DamageEffect(int amount)
        {
            _amount = amount;
        }

        public void ApplyTo(BattleCharacter target)
        {
            target.ChangeHp(-_amount);
        }
    }
}
