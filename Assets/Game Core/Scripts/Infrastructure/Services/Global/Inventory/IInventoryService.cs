using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Inventory
{
    public interface IInventoryService
    {
        bool TryGetItemData(string itemKey, out ItemData itemData);
        bool TryGetItemMetaByKey(string itemKey, out ItemMeta itemMeta);
        bool TryGetItemMetaByID(string itemID, out ItemMeta itemMeta);
        bool TryGetEquippedItemKey(ItemType itemType, out string itemKey);
    }
}