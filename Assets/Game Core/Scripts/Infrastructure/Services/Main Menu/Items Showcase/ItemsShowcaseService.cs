using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.MainMenu.ItemsShowcase
{
    public class ItemsShowcaseService : IItemsShowcaseService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemsShowcaseService(IInventoryService inventoryService) =>
            _inventoryService = inventoryService;

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;

        private ItemType _selectedItemType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemType GetSelectedItemType()
        {
            bool containsDroppedItem = ContainsDroppedItem(out ItemData itemData);

            if (containsDroppedItem)
                _selectedItemType = GetItemType(itemData.ItemID);

            return _selectedItemType;
        }

        public bool ContainsDroppedItem()
        {
            bool containsDroppedItem = ContainsDroppedItem(out ItemData itemData);

            if (!containsDroppedItem)
                return false;

            bool containsItemMeta = _inventoryService.TryGetItemMetaByID(itemData.ItemID, out ItemMeta _);

            if (!containsItemMeta)
                return false;

            return true;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool ContainsDroppedItem(out ItemData itemData) =>
            _inventoryService.TryGetDroppedItemData(out itemData);

        private ItemType GetItemType(string itemID)
        {
            bool containsItemMeta = _inventoryService.TryGetItemMetaByID(itemID, out ItemMeta itemMeta);

            if (!containsItemMeta)
                return _selectedItemType;

            return itemMeta.ItemType;
        }
    }
}