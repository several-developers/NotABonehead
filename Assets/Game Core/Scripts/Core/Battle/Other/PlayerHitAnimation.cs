using GameCore.Battle.Player;
using Zenject;

namespace GameCore.Battle
{
    public class PlayerHitAnimation : HitAnimation
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerTracker playerTracker)
        {
            _playerTracker = playerTracker;

            _playerTracker.OnTakeDamageEvent += OnTakeDamageEvent;
            _playerTracker.OnDiedEvent += OnDiedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private IPlayerTracker _playerTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            _playerTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
            _playerTracker.OnDiedEvent -= OnDiedEvent;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnTakeDamageEvent(int damage) => StartAnimation();

        private void OnDiedEvent() =>
            _playerTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
    }
}