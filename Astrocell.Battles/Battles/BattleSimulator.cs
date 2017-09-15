using Astrocell.Battles.Characters;
using Astrocell.Battles.Players;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public class BattleSimulator
    {
        public BattleSimulator()
            : this(new DebugLog()) { }

        public BattleSimulator(ILog log)
        {
            BattleLog.Instance = log;
        }

        public BattleSide Resolve1V1(CharacterSheet hero, CharacterSheet villain)
        {
            return Resolve1V1(BattleCharacter.Create(BattleSide.Gamer, hero),
                BattleCharacter.Create(BattleSide.Enemy, villain));
        }

        public BattleSide Resolve1V1(BattleCharacter hero, BattleCharacter villain)
        {
            return Battle.Create(new AIPlayer(), new AIPlayer(), hero, villain).Resolve();
        }
    }
}
