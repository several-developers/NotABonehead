﻿using GameCore.Configs;
using GameCore.Factories;
using GameCore.Items;
using GameCore.Battle.Monsters;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.Global
{
    public class AssetsProvider : IAssetsProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public AssetsProvider()
        {
            _itemsMeta = LoadAll<ItemMeta>(path: AssetsPaths.Items);
            _menuPrefabsListMeta = Load<MenuPrefabsListMeta>(path: AssetsPaths.MenuPrefabsListMeta);
            _gameItemPrefabsListMeta = Load<GameItemPrefabsListMeta>(path: AssetsPaths.GameItemPrefabsListMeta);
            _itemsRarityConfigMeta = Load<ItemsRarityConfigMeta>(path: AssetsPaths.ItemsRarityConfigMeta);
            _itemsDropChancesConfigMeta = Load<ItemsDropChancesConfigMeta>(path: AssetsPaths.ItemsDropChancesConfigMeta);
            _allMonstersMeta = LoadAll<MonsterMeta>(path: AssetsPaths.MonstersMeta);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemMeta[] _itemsMeta;
        private readonly MenuPrefabsListMeta _menuPrefabsListMeta;
        private readonly GameItemPrefabsListMeta _gameItemPrefabsListMeta;
        private readonly ItemsRarityConfigMeta _itemsRarityConfigMeta;
        private readonly ItemsDropChancesConfigMeta _itemsDropChancesConfigMeta;
        private readonly MonsterMeta[] _allMonstersMeta;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemMeta[] GetItemsMeta() => _itemsMeta;
        public MenuPrefabsListMeta GetMenuPrefabsListMeta() => _menuPrefabsListMeta;
        public GameItemPrefabsListMeta GetGameItemPrefabsListMeta() => _gameItemPrefabsListMeta;
        public ItemsRarityConfigMeta GetItemsRarityConfigMeta() => _itemsRarityConfigMeta;
        public ItemsDropChancesConfigMeta GetItemsDropChancesConfigMeta() => _itemsDropChancesConfigMeta;
        public MonsterMeta[] GetAllMonstersMeta() => _allMonstersMeta;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        private static T[] LoadAll<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}