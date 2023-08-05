using GameCore.Enums;
using GameCore.Infrastructure.Data;
using GameCore.Infrastructure.Providers.Global.Data;

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

        public bool TryGetEquippedItemKey(ItemType itemType, out string itemKey)
        {
            itemKey = _inventoryData.GetEquippedItemKey(itemType);
            bool isItemKeyValid = !string.IsNullOrEmpty(itemKey);
            return isItemKeyValid;
        }

        public bool TryGetItemData(string itemKey, out ItemData itemData) =>
            _inventoryData.TryGetItemData(itemKey, out itemData);

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