using GameCore.Battle.Player;
using Zenject;

namespace GameCore.UI.BattleScene.HealthBars
{
    public class PlayerBarView : BaseBarView
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerTracker playerTracker)
        {
            _playerTracker = playerTracker;

            _playerTracker.OnHealthChangedEvent += OnHealthChangedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------
        
        private IPlayerTracker _playerTracker;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy() =>
            _playerTracker.OnHealthChangedEvent -= OnHealthChangedEvent;

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnHealthChangedEvent(PlayerStats playerStats)
        {
            float fillValue = playerStats.CurrentHealth / (float)playerStats.MaxHealth;
            SetFill(fillValue);
        }
    }
}