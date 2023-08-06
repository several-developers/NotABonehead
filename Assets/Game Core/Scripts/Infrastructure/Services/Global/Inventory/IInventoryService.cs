using System;
using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Inventory
{
    public interface IInventoryService
    {
        event Action OnItemEquippedEvent;
        event Action OnItemUnEquippedEvent;
        
        void AddItem(string itemID, ItemStats itemStats, bool autoSave = true);
        void SetDroppedItemData(string itemID, ItemStats itemStats, bool autoSave = true);
        void EquipItem(ItemType itemType, string itemKey, bool autoSave = true);
        void UnEquipItem(ItemType itemType, bool autoSave = true);
        void RemoveDroppedItemData(bool autoSave = true);
        IEnumerable<string> GetAllEquippedItemsKeys();
        bool TryGetItemData(string itemKey, out ItemData itemData);
        bool TryGetItemMetaByKey(string itemKey, out ItemMeta itemMeta);
        bool TryGetItemMetaByID(string itemID, out ItemMeta itemMeta);
        bool TryGetEquippedItemKey(ItemType itemType, out string itemKey);
        bool TryGetDroppedItemData(out ItemData itemData);
    }
}