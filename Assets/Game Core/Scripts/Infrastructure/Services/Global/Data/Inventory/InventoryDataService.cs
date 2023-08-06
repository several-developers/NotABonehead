using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.Data;
using GameCore.Items;

namespace GameCore.Infrastructure.Services.Global.Data
{
    public class InventoryDataService : IInventoryDataService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InventoryDataService(ISaveLoadService saveLoadService, IDataProvider dataProvider)
        {
            _saveLoadService = saveLoadService;
            _inventoryData = dataProvider.GetInventoryData();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly ISaveLoadService _saveLoadService;
        private readonly InventoryData _inventoryData;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public string AddItemData(string itemID, ItemStats itemStats, bool autoSave)
        {
            string itemKey = _inventoryData.AddItemData(itemID, itemStats);
            SaveLocalData(autoSave);

            return itemKey;
        }

        public void RemoveItemData(string itemKey, bool autoSave)
        {
            _inventoryData.RemoveItemData(itemKey);
            SaveLocalData(autoSave);
        }

        public void SetDroppedItemData(string itemID, ItemStats itemStats, bool autoSave)
        {
            _inventoryData.SetDroppedItemData(itemID, itemStats);
            SaveLocalData(autoSave);
        }

        public void EquipItem(ItemType itemType, string itemKey, bool autoSave)
        {
            _inventoryData.EquipItem(itemType, itemKey);
            SaveLocalData(autoSave);
        }

        public void UnEquipItem(ItemType itemType, bool autoSave = true)
        {
            _inventoryData.UnEquipItem(itemType);
            SaveLocalData(autoSave);
        }

        public void CreateEquippedItemData(ItemType itemType, bool autoSave)
        {
            _inventoryData.CreateEquippedItemData(itemType);
            SaveLocalData(autoSave);
        }

        public void RemoveDroppedItemData(bool autoSave)
        {
            _inventoryData.RemoveDroppedItemData();
            SaveLocalData(autoSave);
        }

        public IEnumerable<string> GetAllEquippedItemsKeys()
        {
            IEnumerable<EquippedItemData> allEquippedItemsData = _inventoryData.GetAllEquippedItemsData();
            List<string> itemKeys = new(capacity: 6);

            foreach (EquippedItemData equippedItemData in allEquippedItemsData)
            {
                bool isItemKeyValid = !string.IsNullOrEmpty(equippedItemData.ItemKey);

                if (!isItemKeyValid)
                    continue;

                itemKeys.Add(equippedItemData.ItemKey);
            }

            return itemKeys;
        }

        public bool TryGetEquippedItemKey(ItemType itemType, out string itemKey)
        {
            itemKey = _inventoryData.GetEquippedItemKey(itemType);
            bool isItemKeyValid = !string.IsNullOrEmpty(itemKey);
            return isItemKeyValid;
        }

        public bool TryGetItemData(string itemKey, out ItemData itemData) =>
            _inventoryData.TryGetItemData(itemKey, out itemData);

        public bool TryGetDroppedItemData(out ItemData itemData) =>
            _inventoryData.TryGetDroppedItemData(out itemData);

        public bool ContainsEquippedItemData(ItemType itemType) =>
            _inventoryData.ContainsEquippedItemData(itemType);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SaveLocalData(bool autoSave = true)
        {
            if (!autoSave)
                return;

            _saveLoadService.Save();
        }
    }
}