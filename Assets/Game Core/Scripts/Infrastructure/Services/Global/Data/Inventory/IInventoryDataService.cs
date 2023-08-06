using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Data
{
    public interface IInventoryDataService
    {
        void AddItemData(string itemID, ItemStats itemStats, bool autoSave = true);
        void SetDroppedItemData(string itemID, ItemStats itemStats, bool autoSave = true);
        void EquipItem(ItemType itemType, string itemKey, bool autoSave = true);
        void UnEquipItem(ItemType itemType, bool autoSave = true);
        void CreateEquippedItemData(ItemType itemType, bool autoSave = true);
        void RemoveDroppedItemData(bool autoSave = true);
        IEnumerable<string> GetAllEquippedItemsKeys();
        bool TryGetEquippedItemKey(ItemType itemType, out string itemKey);
        bool TryGetItemData(string itemKey, out ItemData itemData);
        bool TryGetDroppedItemData(out ItemData itemData);
        bool ContainsEquippedItemData(ItemType itemType);
    }
}