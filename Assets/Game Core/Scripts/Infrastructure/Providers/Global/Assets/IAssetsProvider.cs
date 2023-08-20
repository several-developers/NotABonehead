using GameCore.Battle.Entities.Monsters;
using GameCore.Factories;
using GameCore.Items;

namespace GameCore.Infrastructure.Providers.Global
{
    public interface IAssetsProvider
    {
        ItemMeta[] GetAllItemsMeta();
        MenuPrefabsListMeta GetMenuPrefabsList();
        GameItemPrefabsListMeta GetGameItemPrefabsList();
        AvailableMonstersListMeta GetAvailableMonstersList();
    }
}