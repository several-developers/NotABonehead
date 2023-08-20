using GameCore.Enums;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Rewards
{
    public interface IRewardsService
    {
        void GiveRandomItem(bool autoSave = true);
        void GiveItem(ItemType itemType, ItemRarity itemRarity, bool autoSave = true);
        void GiveItem(string itemID, ItemStats itemStats, bool autoSave = true);
        void TrySellDroppedItem();
    }
}