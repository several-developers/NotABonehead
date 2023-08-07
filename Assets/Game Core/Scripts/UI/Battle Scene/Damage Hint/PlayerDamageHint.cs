using GameCore.Battle.Player;
using Zenject;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class PlayerDamageHint : DamageHint
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerTracker playerTracker)
        {
            _playerTracker = playerTracker;

            _playerTracker.OnTakeDamageEvent += OnTakeDamageEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private IPlayerTracker _playerTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _playerTracker.OnTakeDamageEvent -= OnTakeDamageEvent;
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnTakeDamageEvent(int damage)
        {
            SetDamage(damage);
            PlayAnimation();
        }
    }
}