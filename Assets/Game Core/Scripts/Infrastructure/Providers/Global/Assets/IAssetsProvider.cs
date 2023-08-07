using GameCore.Configs;
using GameCore.Factories;
using GameCore.Items;
using GameCore.Battle.Monsters;

namespace GameCore.Infrastructure.Providers.Global
{
    public interface IAssetsProvider
    {
        ItemMeta[] GetItemsMeta();
        MenuPrefabsListMeta GetMenuPrefabsListMeta();
        GameItemPrefabsListMeta GetGameItemPrefabsListMeta();
        ItemsRarityConfigMeta GetItemsRarityConfigMeta();
        ItemsDropChancesConfigMeta GetItemsDropChancesConfigMeta();
        MonsterMeta[] GetAllMonstersMeta();
    }
}