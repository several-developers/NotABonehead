using System;

namespace GameCore.Battle
{
    public interface IBattleStateController
    {
        event Action<int> OnRoundChangedEvent;
        void StartBattle();
        void FinishBattle(bool isPlayerWon);
    }
}