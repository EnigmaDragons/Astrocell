
namespace Astrocell.Battles.Battles
{
    public sealed class BattleTurn
    {
        private readonly Battle _battle;
        private readonly BattleCharacter _turnCharacter;

        public BattleTurn(Battle battle, BattleCharacter turnCharacter)
        {
            _battle = battle;
            _turnCharacter = turnCharacter;
        }

        public void Resolve()
        {
            _turnCharacter.DrawForTurn();
        }

        private void DrawCard()
        {
            throw new System.NotImplementedException();
        }
    }
}
