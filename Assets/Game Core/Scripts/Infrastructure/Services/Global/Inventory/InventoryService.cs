using System;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Inventory
{
    public interface IInventoryService
    {
        bool TryGetItemData(string itemKey, out ItemData itemData);
        bool TryGetItemMetaByKey(string itemKey, out ItemMeta itemMeta);
        bool TryGetEquippedItemKey(ItemType itemType, out string itemKey);
    }

    public class InventoryService : IInventoryService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InventoryService(ISaveLoadService saveLoadService, IInventoryDataService inventoryDataService,
            IItemsMetaProvider itemsMetaProvider)
        {
            _saveLoadService = saveLoadService;
            _inventoryDataService = inventoryDataService;
            _itemsMetaProvider = itemsMetaProvider;

            CheckForMissingEquippedItemsData();
            CheckEquippedItemsForCorrectTypes();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ISaveLoadService _saveLoadService;
        private readonly IInventoryDataService _inventoryDataService;
        private readonly IItemsMetaProvider _itemsMetaProvider;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool TryGetItemData(string itemKey, out ItemData itemData) =>
            _inventoryDataService.TryGetItemData(itemKey, out itemData);

        public bool TryGetItemMetaByKey(string itemKey, out ItemMeta itemMeta)
        {
            bool containsItemData = _inventoryDataService.TryGetItemData(itemKey, out ItemData itemData);

            if (containsItemData)
                return _itemsMetaProvider.TryGetItemMeta(itemData.ItemID, out itemMeta);

            itemMeta = null;
            return false;
        }

        public bool TryGetEquippedItemKey(ItemType itemType, out string itemKey) =>
            _inventoryDataService.TryGetEquippedItemKey(itemType, out itemKey);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckForMissingEquippedItemsData()
        {
            Array array = Enum.GetValues(typeof(ItemType));
            bool autoSave = false;

            foreach (ItemType itemType in array)
            {
                bool containsEquippedItemData = _inventoryDataService.ContainsEquippedItemData(itemType);

                if (containsEquippedItemData)
                    continue;

                autoSave = true;
                _inventoryDataService.CreateEquippedItemData(itemType, autoSave: false);
            }

            if (!autoSave)
                return;

            _saveLoadService.Save();
        }

        private void CheckEquippedItemsForCorrectTypes()
        {
            Array array = Enum.GetValues(typeof(ItemType));
            bool autoSave = false;

            foreach (ItemType itemType in array)
                ValidItem(itemType);

            if (!autoSave)
                return;

            _saveLoadService.Save();

            // METHODS: -----------------------------------

            void ValidItem(ItemType itemType)
            {
                bool isItemKeyValid = _inventoryDataService.TryGetEquippedItemKey(itemType, out string itemKey);

                if (!isItemKeyValid)
                    return;

                bool containsItemData = _inventoryDataService.TryGetItemData(itemKey, out ItemData itemData);

                if (!containsItemData)
                {
                    autoSave = true;
                    UnEquipItem(itemType);
                    return;
                }

                bool containsItemMeta = _itemsMetaProvider.TryGetItemMeta(itemData.ItemID, out ItemMeta itemMeta);

                if (!containsItemMeta)
                {
                    autoSave = true;
                    UnEquipItem(itemType);
                    return;
                }

                bool isCorrectMetaType = itemMeta is WearableItemMeta;

                if (!isCorrectMetaType)
                {
                    autoSave = true;
                    UnEquipItem(itemType);
                    return;
                }

                WearableItemMeta wearableItemMeta = (WearableItemMeta)itemMeta;
                bool isCorrectItemType = itemType == wearableItemMeta.ItemType;

                if (isCorrectItemType)
                    return;
                
                autoSave = true;
                UnEquipItem(itemType);
            }

            void UnEquipItem(ItemType itemType) =>
                _inventoryDataService.UnEquipItem(itemType, autoSave: false);
        }
    }
}