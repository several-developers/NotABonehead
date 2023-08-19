using GameCore.Battle.Entities;
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
            _monstersConfig = configsProvider.GetMonstersConfig();
            _currentLevel = gameDataService.GetCurrentLevel();
        }
        
        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private AvailableMonstersListMeta _availableMonstersList;
        private IMonstersDataService _monstersDataService;
        private IMonsterTracker _monsterTracker;
        private MonstersConfigMeta _monstersConfig;
        private int _currentLevel = 1;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            MonsterMeta monsterMeta = GetMonsterMeta();
            GameObject monsterInstance = InstantiatePrefab(monsterMeta.MonsterPrefab);
            MonsterBrain monsterBrain = monsterInstance.AddComponent<MonsterBrain>();
            EntityStats monsterStats = CalculateMonsterStats(monsterMeta);

            monsterBrain.Setup(_monsterTracker, monsterStats);
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

        private GameObject InstantiatePrefab(GameObject prefab)
        {
            GameObject monsterInstance = _diContainer.InstantiatePrefab(prefab, transform);
            monsterInstance.transform.localPosition = Vector3.zero;
            return monsterInstance;
        }

        private EntityStats CalculateMonsterStats(MonsterMeta monsterMeta)
        {
            EntityStats monsterStats = monsterMeta.MonsterStats;

            float health = CalculateStat(monsterStats.Health);
            float damage = CalculateStat(monsterStats.Damage);
            float defense = CalculateStat(monsterStats.Defense);

            EntityStats newMonsterStats = new(health, damage, defense);
            return newMonsterStats;
        }
        
        private float CalculateStat(float baseStat)
        {
            float multiplier = _monstersConfig.MonstersStatsIncreasePerLevel * _currentLevel;
            float newStat = baseStat * multiplier;

            return newStat + baseStat;
        }
    }
}