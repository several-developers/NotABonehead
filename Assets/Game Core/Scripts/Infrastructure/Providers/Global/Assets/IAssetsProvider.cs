using GameCore.Battle.Entities.Monsters;
using GameCore.Factories;
using GameCore.Items;
using UnityEngine;

namespace GameCore.Infrastructure.Providers.Global
{
    public interface IAssetsProvider
    {
        GameObject GetScenesLoaderPrefab();
        ItemMeta[] GetAllItemsMeta();
        MenuPrefabsListMeta GetMenuPrefabsList();
        GameItemPrefabsListMeta GetGameItemPrefabsList();
        AvailableMonstersListMeta GetAvailableMonstersList();
    }
}