using GameCore.Battle.Entities;
using Zenject;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class MonsterDamageHint : DamageHint
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonsterTracker monsterTracker) =>
            EntityTracker = monsterTracker;
    }
}