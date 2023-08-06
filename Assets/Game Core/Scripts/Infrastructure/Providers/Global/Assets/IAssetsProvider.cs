using GameCore.Configs;
using GameCore.Factories;
using GameCore.Items;

namespace GameCore.Infrastructure.Providers.Global
{
    public interface IAssetsProvider
    {
        ItemMeta[] GetItemsMeta();
        MenuPrefabsListMeta GetMenuPrefabsListMeta();
        GameItemPrefabsListMeta GetGameItemPrefabsListMeta();
        ItemsRarityConfigMeta GetItemsRarityConfigMeta();
        ItemsDropChancesConfigMeta GetItemsDropChancesConfigMeta();
    }
}