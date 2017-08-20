using System.Collections.Generic;
using System.Linq;
using Astrocell.Battles.Decks;

namespace Astrocell.Battles.Battles
{
    public sealed class BattleCharacters
    {
        private readonly IList<BattleCharacter> _characters;

        public BattleCharacters(IList<BattleCharacter> characters)
        {
            _characters = characters;
        }

        public IList<BattleCharacter> GetPossibleTargets(BattleCharacter src, EffectTarget target)
        {
            if (target == EffectTarget.Self)
                return src.AsList();
            if (target == EffectTarget.AllEnemies)
                return _characters.Where(x => x.Loyalty != src.Loyalty).ToList();
            if (target == EffectTarget.AllAllies)
                return _characters.Where(x => x.Loyalty == src.Loyalty).ToList();
            if (target == EffectTarget.One)
                return _characters.ToList();
            return new List<BattleCharacter>();
        }
    }
}
