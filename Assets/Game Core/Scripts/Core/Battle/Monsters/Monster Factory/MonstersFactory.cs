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
            IMonstersDataService monstersDataService, IMonsterTracker monsterTracker)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _monstersDataService = monstersDataService;
            _monsterTracker = monsterTracker;
        }
        
        // FIELDS: --------------------------------------------------------------------------------

        private DiContainer _diContainer;
        private IAssetsProvider _assetsProvider;
        private IMonstersDataService _monstersDataService;
        private IMonsterTracker _monsterTracker;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Create()
        {
            MonsterMeta monsterMeta = GetMonsterMeta();
            GameObject monsterInstance = _diContainer.InstantiatePrefab(monsterMeta.MonsterPrefab, transform);
            MonsterBrain monsterBrain = monsterInstance.AddComponent<MonsterBrain>();
            monsterBrain.Setup(_monsterTracker, monsterMeta.Health, monsterMeta.Damage);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private MonsterMeta GetMonsterMeta()
        {
            MonsterMeta[] allMonstersMeta = _assetsProvider.GetAllMonstersMeta();
            int monsterIndex = _monstersDataService.GetCurrentMonsterIndex();
            monsterIndex = Mathf.Clamp(monsterIndex, 0, allMonstersMeta.Length - 1);
            return allMonstersMeta[monsterIndex];
        }
    }
}