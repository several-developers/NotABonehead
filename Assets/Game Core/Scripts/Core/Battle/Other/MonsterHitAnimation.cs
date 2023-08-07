using GameCore.Battle.Monsters;
using Zenject;

namespace GameCore.Battle
{
    public class MonsterHitAnimation : HitAnimation
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IMonsterTracker monsterTracker)
        {
            _monsterTracker = monsterTracker;

            _monsterTracker.OnTakeDamageEvent += OnTakeDamageEvent;
            _monsterTracker.OnDiedEvent += OnDiedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private IMonsterTracker _monsterTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void OnDestroy()
        {
            _monsterTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
            _monsterTracker.OnDiedEvent -= OnDiedEvent;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnTakeDamageEvent(int damage) => StartAnimation();
        
        private void OnDiedEvent() =>
            _monsterTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
    }
}