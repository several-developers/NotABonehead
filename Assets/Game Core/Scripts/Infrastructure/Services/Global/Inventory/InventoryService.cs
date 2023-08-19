using System;
using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Events;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.ItemsMeta;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Infrastructure.Services.Global.Rewards;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Inventory
{
    public class InventoryService : IInventoryService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InventoryService(ISaveLoadService saveLoadService, IInventoryDataService inventoryDataService,
            IItemsMetaProvider itemsMetaProvider, IPlayerDataService playerDataService)
        {
            _saveLoadService = saveLoadService;
            _inventoryDataService = inventoryDataService;
            _itemsMetaProvider = itemsMetaProvider;
            _playerDataService = playerDataService;

            CheckForMissingEquippedItemsData();
            CheckEquippedItemsForCorrectTypes();
        }

        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnItemEquippedEvent;
        public event Action OnItemUnEquippedEvent;
        public event Action OnReceivedDroppedItemEvent;
        public event Action OnRemovedDroppedItemEvent;

        private readonly ISaveLoadService _saveLoadService;
        private readonly IInventoryDataService _inventoryDataService;
        private readonly IItemsMetaProvider _itemsMetaProvider;
        private readonly IPlayerDataService _playerDataService;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void AddItem(string itemID, ItemStats itemStats, out string itemKey, bool autoSave) =>
            _inventoryDataService.AddItemData(itemID, itemStats, out itemKey, autoSave);

        public void RemoveItemData(string itemKey, bool autoSave) =>
            _inventoryDataService.RemoveItemData(itemKey, autoSave);

        public void SetDroppedItemData(string itemID, ItemStats itemStats, bool autoSave)
        {
            _inventoryDataService.SetDroppedItemData(itemID, itemStats, autoSave);
            OnReceivedDroppedItemEvent?.Invoke();
        }

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

        public void EquipDroppedItem(bool autoSave)
        {
            bool isSuccessfulEquipped = EquipDroppedItemLogic();

            if (!isSuccessfulEquipped)
                return;
            
            SaveLocalData(autoSave);
        }
        
        public void RemoveDroppedItemData(bool autoSave)
        {
            _inventoryDataService.RemoveDroppedItemData(autoSave);
            OnRemovedDroppedItemEvent?.Invoke();
        }

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

            SaveLocalData(autoSave);
        }

        private void CheckEquippedItemsForCorrectTypes()
        {
            Array array = Enum.GetValues(typeof(ItemType));
            bool autoSave = false;

            foreach (ItemType itemType in array)
                ValidItem(itemType);
            
            SaveLocalData(autoSave);

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

                bool isCorrectItemType = itemType == itemMeta.ItemType;

                if (isCorrectItemType)
                    return;

                autoSave = true;
                UnEquip(itemType);
            }

            void UnEquip(ItemType itemType) =>
                _inventoryDataService.UnEquipItem(itemType, autoSave: false);
        }

        private bool EquipDroppedItemLogic()
        {
            bool isDroppedItemExists = TryGetDroppedItemData(out ItemData droppedItemData);

            if (!isDroppedItemExists)
                return false;

            bool isDroppedItemMetaExists = TryGetItemMetaByID(droppedItemData.ItemID, out ItemMeta droppedItemMeta);

            if (!isDroppedItemMetaExists)
                return false;

            bool isEquippedItemExists = TryGetEquippedItemKey(droppedItemMeta.ItemType, out string equippedItemKey);

            if (isEquippedItemExists)
            {
                bool isEquippedDataExists = TryGetItemData(equippedItemKey, out ItemData equippedItemData);

                if (!isEquippedDataExists)
                    return false;

                int goldReward = RewardsService.CalculateItemGoldReward(equippedItemData.ItemStats);
                _playerDataService.AddGold(goldReward, autoSave: false);
                
                RemoveItemData(equippedItemKey, autoSave: false);
                GlobalEvents.SendCurrencyChanged();
            }
            
            AddItem(droppedItemMeta.ItemID, droppedItemData.ItemStats, out string newItemKey, autoSave: false);
            EquipItem(droppedItemMeta.ItemType, newItemKey, autoSave: false);
            RemoveDroppedItemData(autoSave: false);

            return true;
        }

        private void SaveLocalData(bool autoSave = true)
        {
            if (!autoSave)
                return;

            _saveLoadService.Save();
        }
    }
}