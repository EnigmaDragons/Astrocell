using Astrocell.Battles.Characters;
using Astrocell.Battles.Players;
using MonoDragons.Core.Logs;

namespace Astrocell.Battles.Battles
{
    public class BattleSimulator
    {
        static BattleSimulator()
        {
            BattleLog.Instance = new DebugLog();
        }

        public BattleSide Resolve1V1(CharacterSheet hero, CharacterSheet villain)
        {
            return Battle.Create(new AiPlayer(), new AiPlayer(),
                BattleCharacter.Init(BattleSide.Gamer, hero),
                BattleCharacter.Init(BattleSide.Enemy, villain)).Resolve();
        }
    }
}
