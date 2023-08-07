using GameCore.Battle.Monsters;
using Zenject;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class MonsterDamageHint : DamageHint
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonsterTracker monsterTracker)
        {
            _monsterTracker = monsterTracker;

            _monsterTracker.OnTakeDamageEvent += OnTakeDamageEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private IMonsterTracker _monsterTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _monsterTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnTakeDamageEvent(int damage)
        {
            SetDamage(damage);
            PlayAnimation();
        }
    }
}