using System;
using System.Collections.Generic;
using GameCore.Enums;
using GameCore.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class InventoryData : DataBase
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InventoryData()
        {
            _droppedItemData = new ItemData();
            _itemsData = new List<ItemData>(capacity: 3);
            _equippedItemsData = new List<EquippedItemData>(capacity: 3);
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup("Dropped Item Data")]
        [BoxGroup("Dropped Item Data/In", showLabel: false), SerializeField]
        private ItemData _droppedItemData;

        [Title("Items Data")]
        [SerializeField, Space(5)]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<ItemData> _itemsData;

        [Title("Equipped Items Data")]
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<EquippedItemData> _equippedItemsData;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string DataKey => Constants.InventoryDataKey;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void AddItemData(string itemID, ItemStats itemStats)
        {
            ItemData itemData = new(itemID, itemStats);
            _itemsData.Add(itemData);
        }

        public void SetDroppedItemData(string itemID, ItemStats itemStats)
        {
            _droppedItemData.SetItemID(itemID);
            _droppedItemData.SetItemStats(itemStats);
        }

        public void RemoveDroppedItemData() =>
            _droppedItemData.SetItemID(string.Empty);

        public void EquipItem(ItemType itemType, string itemKey)
        {
            foreach (EquippedItemData equippedItemData in _equippedItemsData)
            {
                if (equippedItemData.ItemType != itemType)
                    continue;

                equippedItemData.SetItemKey(itemKey);
                break;
            }
        }

        public void UnEquipItem(ItemType itemType)
        {
            foreach (EquippedItemData equippedItemData in _equippedItemsData)
            {
                if (equippedItemData.ItemType != itemType)
                    continue;

                equippedItemData.SetItemKey(string.Empty);
                break;
            }
        }

        public void CreateEquippedItemData(ItemType itemType)
        {
            EquippedItemData equippedItemData = new(itemType);
            _equippedItemsData.Add(equippedItemData);
        }

        public IEnumerable<EquippedItemData> GetAllEquippedItemsData() => _equippedItemsData;

        public string GetEquippedItemKey(ItemType itemType)
        {
            foreach (EquippedItemData equippedItemData in _equippedItemsData)
            {
                if (equippedItemData.ItemType == itemType)
                    return equippedItemData.ItemKey;
            }
            
            return string.Empty;
        }

        public bool TryGetItemData(string itemKey, out ItemData result)
        {
            foreach (ItemData itemData in _itemsData)
            {
                bool matches = string.Equals(itemData.ItemKey, itemKey);

                if (!matches)
                    continue;

                result = itemData;
                return true;
            }

            result = null;
            return false;
        }

        public bool TryGetDroppedItemData(out ItemData itemData)
        {
            itemData = _droppedItemData;
            bool isItemDataValid = !string.IsNullOrEmpty(_droppedItemData.ItemKey);
            return isItemDataValid;
        }

        public bool ContainsEquippedItemData(ItemType itemType)
        {
            foreach (EquippedItemData equippedItemData in _equippedItemsData)
            {
                if (equippedItemData.ItemType == itemType)
                    return true;
            }

            return false;
        }
    }
}