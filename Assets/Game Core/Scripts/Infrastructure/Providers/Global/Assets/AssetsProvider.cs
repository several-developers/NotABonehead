using GameCore.Battle.Entities.Monsters;
using GameCore.Factories;
using GameCore.Items;
using GameCore.Utilities;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.Global
{
    public class AssetsProvider : AssetsProviderBase, IAssetsProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public AssetsProvider()
        {
            _scenesLoaderPrefab = Load<GameObject>(path: AssetsPaths.ScenesLoaderPrefab);
            _itemsMeta = LoadAll<ItemMeta>(path: AssetsPaths.Items);
            _menuPrefabsList = Load<MenuPrefabsListMeta>(path: AssetsPaths.MenuPrefabsList);
            _gameItemPrefabsList = Load<GameItemPrefabsListMeta>(path: AssetsPaths.GameItemPrefabsList);
            _availableMonstersList = Load<AvailableMonstersListMeta>(path: AssetsPaths.AvailableMonstersList);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly GameObject _scenesLoaderPrefab;
        private readonly ItemMeta[] _itemsMeta;
        private readonly MenuPrefabsListMeta _menuPrefabsList;
        private readonly GameItemPrefabsListMeta _gameItemPrefabsList;
        private readonly AvailableMonstersListMeta _availableMonstersList;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public GameObject GetScenesLoaderPrefab() => _scenesLoaderPrefab;
        public ItemMeta[] GetAllItemsMeta() => _itemsMeta;
        public MenuPrefabsListMeta GetMenuPrefabsList() => _menuPrefabsList;
        public GameItemPrefabsListMeta GetGameItemPrefabsList() => _gameItemPrefabsList;
        public AvailableMonstersListMeta GetAvailableMonstersList() => _availableMonstersList;
    }
}