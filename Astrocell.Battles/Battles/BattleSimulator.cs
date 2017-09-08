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
            return Battle.Create(new AIPlayer(), new AIPlayer(),
                BattleCharacter.Init(BattleSide.Gamer, hero),
                BattleCharacter.Init(BattleSide.Enemy, villain)).Resolve();
        }
    }
}
