using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Infrastructure.Services.Global.Inventory;
using GameCore.Items;
using UnityEngine;

namespace GameCore.UI.MainMenu.InventoryMenu
{
    public class ItemSlotLogic
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemSlotLogic(IInventoryService inventoryService, IAssetsProvider assetsProvider,
            ItemSlotVisualizer itemSlotVisualizer, ItemType itemType)
        {
            _inventoryService = inventoryService;
            _assetsProvider = assetsProvider;
            _itemSlotVisualizer = itemSlotVisualizer;
            _itemType = itemType;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ItemSlotVisualizer _itemSlotVisualizer;
        private readonly ItemType _itemType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateSlotInfo()
        {
            bool isItemSlotValid = IsItemSlotValid(out WearableItemMeta itemMeta, out ItemData itemData);
           
            if (!isItemSlotValid)
            {
                ResetItem();
                return;
            }

            UpdateInfo(itemMeta, itemData);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateInfo(WearableItemMeta itemMeta, ItemData itemData)
        {
            int itemLevel = itemData.Level;
            Sprite itemIcon = itemMeta.Icon;
            
            _itemSlotVisualizer.SetItemIcon(itemIcon);
            _itemSlotVisualizer.SetItemLevel(itemLevel);
            _itemSlotVisualizer.SetItemAvailableState(isAvailable: true);
        }

        private void ResetItem() =>
            _itemSlotVisualizer.SetItemAvailableState(isAvailable: false);

        private bool TryGetEquippedItemKey(out string itemKey) =>
            _inventoryService.TryGetEquippedItemKey(_itemType, out itemKey);

        private bool TryGetItemMeta(string itemKey, out WearableItemMeta result)
        {
            bool containsItemMeta = _inventoryService.TryGetItemMetaByKey(itemKey, out ItemMeta itemMeta);
            result = null;

            if (!containsItemMeta)
                return false;

            if (itemMeta is not WearableItemMeta wearableItemMeta)
                return false;

            result = wearableItemMeta;
            return true;
        }

        private bool TryGetItemData(string itemKey, out ItemData itemData) =>
            _inventoryService.TryGetItemData(itemKey, out itemData);

        private bool IsItemSlotValid(out WearableItemMeta itemMeta, out ItemData itemData)
        {
            bool containsEquippedItemKey = TryGetEquippedItemKey(out string itemKey);
            itemMeta = null;
            itemData = null;

            if (!containsEquippedItemKey)
                return false;

            bool containsItemMeta = TryGetItemMeta(itemKey, out itemMeta);

            if (!containsItemMeta)
                return false;

            bool containsItemData = TryGetItemData(itemKey, out itemData);

            if (!containsItemData)
                return false;

            return true;
        }
    }
}