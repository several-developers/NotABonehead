using System;
using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Inventory
{
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
        
        public event Action OnItemEquippedEvent;
        public event Action OnItemUnEquippedEvent;

        private readonly ISaveLoadService _saveLoadService;
        private readonly IInventoryDataService _inventoryDataService;
        private readonly IItemsMetaProvider _itemsMetaProvider;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void AddItem(string itemID, ItemStats itemStats, bool autoSave) =>
            _inventoryDataService.AddItemData(itemID, itemStats, autoSave);

        public void SetDroppedItemData(string itemID, ItemStats itemStats, bool autoSave) =>
            _inventoryDataService.SetDroppedItemData(itemID, itemStats, autoSave);

        public void EquipItem(ItemType itemType, string itemKey, bool autoSave)
        {
            _inventoryDataService.EquipItem(itemType, itemKey, autoSave);
            OnItemEquippedEvent?.Invoke();
        }

        public void UnEquipItem(ItemType itemType, bool autoSave)
        {
            _inventoryDataService.UnEquipItem(itemType, autoSave);
            OnItemUnEquippedEvent?.Invoke();
        }

        public void RemoveDroppedItemData(bool autoSave) =>
            _inventoryDataService.RemoveDroppedItemData(autoSave);

        public IEnumerable<string> GetAllEquippedItemsKeys() =>
            _inventoryDataService.GetAllEquippedItemsKeys();

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
        
        public bool TryGetItemMetaByID(string itemID, out ItemMeta itemMeta) =>
            _itemsMetaProvider.TryGetItemMeta(itemID, out itemMeta);

        public bool TryGetEquippedItemKey(ItemType itemType, out string itemKey) =>
            _inventoryDataService.TryGetEquippedItemKey(itemType, out itemKey);

        public bool TryGetDroppedItemData(out ItemData itemData) =>
            _inventoryDataService.TryGetDroppedItemData(out itemData);

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
                    UnEquip(itemType);
                    return;
                }

                bool containsItemMeta = _itemsMetaProvider.TryGetItemMeta(itemData.ItemID, out ItemMeta itemMeta);

                if (!containsItemMeta)
                {
                    autoSave = true;
                    UnEquip(itemType);
                    return;
                }

                bool isCorrectMetaType = itemMeta is WearableItemMeta;

                if (!isCorrectMetaType)
                {
                    autoSave = true;
                    UnEquip(itemType);
                    return;
                }

                WearableItemMeta wearableItemMeta = (WearableItemMeta)itemMeta;
                bool isCorrectItemType = itemType == wearableItemMeta.ItemType;

                if (isCorrectItemType)
                    return;
                
                autoSave = true;
                UnEquip(itemType);
            }

            void UnEquip(ItemType itemType) =>
                _inventoryDataService.UnEquipItem(itemType, autoSave: false);
        }
    }
}