using GameCore.Configs;
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

        public ItemSlotLogic(IInventoryService inventoryService, IConfigsProvider configsProvider,
            ItemSlotVisualizer itemSlotVisualizer, ItemType itemType)
        {
            _inventoryService = inventoryService;
            _itemsRarityConfig = configsProvider.GetItemsRarityConfig();
            _itemSlotVisualizer = itemSlotVisualizer;
            _itemType = itemType;

            _inventoryService.OnItemEquippedEvent += OnItemEquippedEvent;
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly IInventoryService _inventoryService;
        private readonly ItemsRarityConfigMeta _itemsRarityConfig;
        private readonly ItemSlotVisualizer _itemSlotVisualizer;
        private readonly ItemType _itemType;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void UpdateSlotInfo()
        {
            bool isItemSlotValid = IsItemSlotValid(out ItemMeta itemMeta, out ItemData itemData);
           
            if (!isItemSlotValid)
            {
                ResetItem();
                return;
            }

            UpdateInfo(itemMeta, itemData);
        }

        public void Dispose() =>
            _inventoryService.OnItemEquippedEvent -= OnItemEquippedEvent;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateInfo(ItemMeta itemMeta, ItemData itemData)
        {
            ItemRarity itemRarity = itemData.ItemStats.Rarity;
            int itemLevel = itemData.ItemStats.Level;
            Sprite itemIcon = itemMeta.Icon;
            
            ItemRarityConfig itemRarityConfig = _itemsRarityConfig.GetItemRarityConfig(itemRarity);
            Sprite rarityFrame = itemRarityConfig.RarityFrame;
            
            _itemSlotVisualizer.SetItemIcon(itemIcon);
            _itemSlotVisualizer.SetFrameImage(rarityFrame);
            _itemSlotVisualizer.SetItemLevel(itemLevel);
            _itemSlotVisualizer.SetItemAvailableState(isAvailable: true);
        }

        private void ResetItem() =>
            _itemSlotVisualizer.SetItemAvailableState(isAvailable: false);

        private bool TryGetEquippedItemKey(out string itemKey) =>
            _inventoryService.TryGetEquippedItemKey(_itemType, out itemKey);

        private bool TryGetItemMeta(string itemKey, out ItemMeta result) =>
            _inventoryService.TryGetItemMetaByKey(itemKey, out result);

        private bool TryGetItemData(string itemKey, out ItemData itemData) =>
            _inventoryService.TryGetItemData(itemKey, out itemData);

        private bool IsItemSlotValid(out ItemMeta itemMeta, out ItemData itemData)
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

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnItemEquippedEvent() => UpdateSlotInfo();
    }
}