using GameCore.Configs;
using GameCore.Infrastructure.Providers.BattleScene.BattleAssets;
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
            IBattleAssetsProvider battleAssetsProvider)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _monstersDataService = monstersDataService;
            _monsterTracker = monsterTracker;
            _gameDataService = gameDataService;
            _battleAssetsProvider = battleAssetsProvider;
        }
        
        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IAssetsProvider _assetsProvider;
        private IMonstersDataService _monstersDataService;
        private IMonsterTracker _monsterTracker;
        private IGameDataService _gameDataService;
        private IBattleAssetsProvider _battleAssetsProvider;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            MonsterMeta monsterMeta = GetMonsterMeta();
            GameObject monsterInstance = _diContainer.InstantiatePrefab(monsterMeta.MonsterPrefab, transform);
            MonsterBrain monsterBrain = monsterInstance.AddComponent<MonsterBrain>();

            int health = CalculateStat(monsterMeta.Health);
            int damage = CalculateStat(monsterMeta.Damage);
            
            monsterBrain.Setup(_monsterTracker, health, damage);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private MonsterMeta GetMonsterMeta()
        {
            MonsterMeta[] allMonstersMeta = _assetsProvider.GetAllMonstersMeta();
            int monsterIndex = _monstersDataService.GetCurrentMonsterIndex();
            monsterIndex = Mathf.Clamp(monsterIndex, 0, allMonstersMeta.Length - 1);
            return allMonstersMeta[monsterIndex];
        }

        private int CalculateStat(int baseStat)
        {
            BattleStageConfigMeta battleStageConfig = _battleAssetsProvider.GetBattleStageConfigMeta();
            int level = _gameDataService.GetCurrentLevel();

            float multiplier = battleStageConfig.StatsIncreasePerLevel * level;
            float newStat = baseStat * multiplier;

            return (int)newStat + baseStat;
        }
    }
}