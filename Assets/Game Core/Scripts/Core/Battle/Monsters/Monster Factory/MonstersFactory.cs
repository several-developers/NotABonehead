using GameCore.Configs;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Data;
using UnityEngine;
using Zenject;

namespace GameCore.Battle.Monsters
{
    public class MonstersFactory : MonoBehaviour, IMonstersFactory
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(DiContainer diContainer, IAssetsProvider assetsProvider,
            IMonstersDataService monstersDataService, IMonsterTracker monsterTracker, IGameDataService gameDataService,
            IConfigsProvider configsProvider)
        {
            _diContainer = diContainer;
            _availableMonstersList = assetsProvider.GetAvailableMonstersList();
            _monstersDataService = monstersDataService;
            _monsterTracker = monsterTracker;
            _gameDataService = gameDataService;
            _configsProvider = configsProvider;
        }
        
        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private AvailableMonstersListMeta _availableMonstersList;
        private IMonstersDataService _monstersDataService;
        private IMonsterTracker _monsterTracker;
        private IGameDataService _gameDataService;
        private IConfigsProvider _configsProvider;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            MonsterMeta monsterMeta = GetMonsterMeta();
            GameObject monsterInstance = InstantiatePrefab(monsterMeta.MonsterPrefab);
            MonsterBrain monsterBrain = monsterInstance.AddComponent<MonsterBrain>();

            monsterInstance.transform.localPosition = Vector3.zero;
            
            int health = CalculateStat(monsterMeta.Health);
            int damage = CalculateStat(monsterMeta.Damage);
            
            monsterBrain.Setup(_monsterTracker, health, damage);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private MonsterMeta GetMonsterMeta()
        {
            int monstersAmount = _availableMonstersList.GetMonstersAmount();
            int monsterIndex = _monstersDataService.GetCurrentMonsterIndex();
            monsterIndex = Mathf.Clamp(monsterIndex, 0, monstersAmount - 1);
            
            MonsterMeta monsterMeta = _availableMonstersList.GetMonsterMetaByIndex(monsterIndex);
            return monsterMeta;
        }

        private GameObject InstantiatePrefab(GameObject prefab) =>
            _diContainer.InstantiatePrefab(prefab, transform);

        private int CalculateStat(int baseStat)
        {
            BattleStageConfigMeta battleStageConfig = _configsProvider.GetBattleStageConfig();
            int level = _gameDataService.GetCurrentLevel();

            float multiplier = battleStageConfig.MonstersStatsIncreasePerLevel * level;
            float newStat = baseStat * multiplier;

            return (int)newStat + baseStat;
        }
    }
}