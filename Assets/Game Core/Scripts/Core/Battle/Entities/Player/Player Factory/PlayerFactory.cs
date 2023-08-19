using GameCore.Battle.Entities;
using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Player;
using UnityEngine;
using Zenject;

namespace GameCore.Battle.Player
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IPlayerTracker playerTracker, IConfigsProvider configsProvider,
            IPlayerStatsCalculatorService playerStatsCalculatorService)
        {
            _diContainer = diContainer;
            _playerTracker = playerTracker;
            _playerStatsCalculatorService = playerStatsCalculatorService;
            _playerConfig = configsProvider.GetPlayerConfig();
        }
        
        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IPlayerTracker _playerTracker;
        private IPlayerStatsCalculatorService _playerStatsCalculatorService;
        private PlayerConfigMeta _playerConfig;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            GameObject playerInstance = InstantiatePrefab();
            PlayerBrain playerBrain = playerInstance.AddComponent<PlayerBrain>();
            EntityStats playerStats = _playerStatsCalculatorService.GetPlayerStats();
            
            playerBrain.Setup(_playerTracker, playerStats);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private GameObject InstantiatePrefab() =>
            _diContainer.InstantiatePrefab(_playerConfig.PlayerPrefab, transform);
    }
}