using GameCore.Battle.Monsters;
using GameCore.Factories;
using GameCore.Items;
using GameCore.Utilities;

namespace GameCore.Infrastructure.Providers.Global
{
    public class AssetsProvider : AssetsProviderBase, IAssetsProvider
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public AssetsProvider()
        {
            _itemsMeta = LoadAll<ItemMeta>(path: AssetsPaths.Items);
            _menuPrefabsListMeta = Load<MenuPrefabsListMeta>(path: AssetsPaths.MenuPrefabsListMeta);
            _gameItemPrefabsListMeta = Load<GameItemPrefabsListMeta>(path: AssetsPaths.GameItemPrefabsListMeta);
            _allMonstersMeta = LoadAll<MonsterMeta>(path: AssetsPaths.MonstersMeta);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ItemMeta[] _itemsMeta;
        private readonly MenuPrefabsListMeta _menuPrefabsListMeta;
        private readonly GameItemPrefabsListMeta _gameItemPrefabsListMeta;
        private readonly MonsterMeta[] _allMonstersMeta;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemMeta[] GetAllItemsMeta() => _itemsMeta;
        public MenuPrefabsListMeta GetMenuPrefabsListMeta() => _menuPrefabsListMeta;
        public GameItemPrefabsListMeta GetGameItemPrefabsListMeta() => _gameItemPrefabsListMeta;
        public MonsterMeta[] GetAllMonstersMeta() => _allMonstersMeta;
    }
}