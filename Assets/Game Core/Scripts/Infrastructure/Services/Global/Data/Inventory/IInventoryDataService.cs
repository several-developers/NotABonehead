using GameCore.Enums;
using GameCore.Infrastructure.Data;

namespace GameCore.Infrastructure.Services.Global.Data
{
    public interface IInventoryDataService
    {
        void EquipItem(ItemType itemType, string itemKey, bool autoSave = true);
        void UnEquipItem(ItemType itemType, bool autoSave = true);
        void CreateEquippedItemData(ItemType itemType, bool autoSave = true);
        bool TryGetEquippedItemKey(ItemType itemType, out string itemKey);
        bool TryGetItemData(string itemKey, out ItemData itemData);
        bool ContainsEquippedItemData(ItemType itemType);
    }
}