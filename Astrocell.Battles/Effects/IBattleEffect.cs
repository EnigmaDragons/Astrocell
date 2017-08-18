using Astrocell.Battles.Battles;

namespace Astrocell.Battles.Effects
{
    public interface IBattleEffect
    {
        void ApplyTo(BattleCharacter target);
    }
}
