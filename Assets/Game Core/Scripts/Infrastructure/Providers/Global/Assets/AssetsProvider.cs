using GameCore.Configs;
using GameCore.Factories;
using GameCore.Items;
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
            _itemsRarityConfigMeta = Load<ItemsRarityConfigMeta>(path: AssetsPaths.ItemsRarityConfigMeta);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemMeta[] _itemsMeta;
        private readonly MenuPrefabsListMeta _menuPrefabsListMeta;
        private readonly ItemsRarityConfigMeta _itemsRarityConfigMeta;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemMeta[] GetItemsMeta() => _itemsMeta;
        public MenuPrefabsListMeta GetMenuPrefabsListMeta() => _menuPrefabsListMeta;
        public ItemsRarityConfigMeta GetItemsRarityConfigMeta() => _itemsRarityConfigMeta;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static T Load<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        private static T[] LoadAll<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}