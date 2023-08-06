using GameCore.Enums;
using GameCore.Factories;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using GameCore.UI.MainMenu.ItemsShowcaseMenu;

namespace GameCore.Infrastructure.Services.MainMenu.ItemsShowcase
{
    public interface IItemsShowcaseService
    {
        void CheckIfContainsDroppedItem();
        void SetSelectedItemType(ItemType itemType);
        ItemType GetSelectedItemType();
        bool ContainsDroppedItem();
    }

    public class ItemsShowcaseService : IItemsShowcaseService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemsShowcaseService(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;

        private ItemType _selectedItemType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void CheckIfContainsDroppedItem()
        {
            bool containsDroppedItem = ContainsDroppedItem(out ItemData itemData);

            if (!containsDroppedItem)
                return;

            _selectedItemType = GetItemType(itemData.ItemID);
            
            MenuFactory.Create<ItemsShowcaseMenuView>();
        }
        
        public void SetSelectedItemType(ItemType itemType) =>
            _selectedItemType = itemType;

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
            
            bool containsItemMeta = _inventoryService.TryGetItemMetaByID(itemData.ItemID, out ItemMeta itemMeta);

            if (!containsItemMeta)
                return false;

            bool isCorrectType = itemMeta is WearableItemMeta;

            if (!isCorrectType)
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

            bool isCorrectType = itemMeta is WearableItemMeta;

            if (!isCorrectType)
                return _selectedItemType;

            var wearableItemMeta = (WearableItemMeta)itemMeta;
            return wearableItemMeta.ItemType;
        }
    }
}