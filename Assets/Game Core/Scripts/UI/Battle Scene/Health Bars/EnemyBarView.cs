using GameCore.Battle.Entities;
using Zenject;

namespace GameCore.UI.BattleScene.HealthBars
{
    public class EnemyBarView : BaseBarView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonsterTracker monsterTracker) =>
            EntityTracker = monsterTracker;
    }
}