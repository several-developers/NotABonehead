using GameCore.Battle.Monsters;
using Zenject;

namespace GameCore.UI.BattleScene.HealthBars
{
    public class EnemyBarView : BaseBarView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonsterTracker monsterTracker)
        {
            _monsterTracker = monsterTracker;

            _monsterTracker.OnHealthChangedEvent += OnHealthChangedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private IMonsterTracker _monsterTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _monsterTracker.OnHealthChangedEvent -= OnHealthChangedEvent;

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnHealthChangedEvent(MonsterStats monsterStats)
        {
            float fillValue = monsterStats.CurrentHealth / (float)monsterStats.MaxHealth;
            SetFill(fillValue);
        }
    }
}