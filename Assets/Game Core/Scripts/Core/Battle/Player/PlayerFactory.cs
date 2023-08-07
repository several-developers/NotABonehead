using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace GameCore.Battle.Player
{
    public class PlayerFactory : MonoBehaviour, IPlayerFactory
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IAssetsProvider assetsProvider,
            IMonstersDataService monstersDataService, IPlayerTracker playerTracker)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _monstersDataService = monstersDataService;
            _playerTracker = playerTracker;
        }

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private int _health = 10;

        [SerializeField, Min(0)]
        private int _damage = 1;

        [Title(Constants.References)]
        [SerializeField, Required]
        private GameObject _playerPrefab;

        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IAssetsProvider _assetsProvider;
        private IMonstersDataService _monstersDataService;
        private IPlayerTracker _playerTracker;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            GameObject playerInstance = _diContainer.InstantiatePrefab(_playerPrefab, transform);
            PlayerBrain monsterBrain = playerInstance.AddComponent<PlayerBrain>();
            monsterBrain.Setup(_playerTracker, _health, _damage);
        }
    }
}