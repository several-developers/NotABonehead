using GameCore.Battle.Entities;
using Zenject;

namespace GameCore.UI.BattleScene.HealthBars
{
    public class PlayerBarView : BaseBarView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerTracker playerTracker) =>
            EntityTracker = playerTracker;
    }
}