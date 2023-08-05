using System;
using System.Collections.Generic;
using GameCore.Enums;
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
            _itemsData = new List<ItemData>(capacity: 3);
            _equippedItemsData = new List<EquippedItemData>(capacity: 3);
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Items Data")]
        [SerializeField, Space(-10)]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<ItemData> _itemsData;

        [Title("Equipped Items Data")]
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<EquippedItemData> _equippedItemsData;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string DataKey => Constants.InventoryDataKey;

        // PUBLIC METHODS: ------------------------------------------------------------------------

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