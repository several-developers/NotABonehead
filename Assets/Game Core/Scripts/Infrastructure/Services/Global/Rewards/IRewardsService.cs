using GameCore.Enums;
using GameCore.Items;
using GameCore.UI.MainMenu.GameItems;
using UnityEngine;

namespace GameCore.Infrastructure.Services.Global.Rewards
{
    public interface IRewardsService
    {
        void GiveItemReward(string itemID, ItemStats itemStats, bool autoSave = true);
        bool GiveItemReward(Transform container, ItemType itemType, ItemRarity itemRarity, out GameItemView gameItemView);
        bool GiveRandomItemReward(Transform container, out GameItemView gameItemView);
    }
}