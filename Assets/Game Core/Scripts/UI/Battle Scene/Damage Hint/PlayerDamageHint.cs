using GameCore.Battle.Entities;
using Zenject;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class PlayerDamageHint : DamageHint
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerTracker playerTracker) =>
            EntityTracker = playerTracker;
    }
}