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
            _menuPrefabsListMeta = Load<MenuPrefabsListMeta>(path: AssetsPaths.MenuPrefabsListMeta);
            _gameItemPrefabsListMeta = Load<GameItemPrefabsListMeta>(path: AssetsPaths.GameItemPrefabsListMeta);
            _availableMonstersList = Load<AvailableMonstersListMeta>(path: AssetsPaths.AvailableMonstersList);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly GameObject _scenesLoaderPrefab;
        private readonly ItemMeta[] _itemsMeta;
        private readonly MenuPrefabsListMeta _menuPrefabsListMeta;
        private readonly GameItemPrefabsListMeta _gameItemPrefabsListMeta;
        private readonly AvailableMonstersListMeta _availableMonstersList;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public GameObject GetScenesLoaderPrefab() => _scenesLoaderPrefab;
        public ItemMeta[] GetAllItemsMeta() => _itemsMeta;
        public MenuPrefabsListMeta GetMenuPrefabsList() => _menuPrefabsListMeta;
        public GameItemPrefabsListMeta GetGameItemPrefabsList() => _gameItemPrefabsListMeta;
        public AvailableMonstersListMeta GetAvailableMonstersList() => _availableMonstersList;
    }
}