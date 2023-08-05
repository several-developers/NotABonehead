using System;
using GameCore.Enums;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class EquippedItemData
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public EquippedItemData()
        {
        }

        public EquippedItemData(ItemType itemType) =>
            _itemType = itemType;

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemType _itemType;

        [SerializeField]
        private string _itemKey;

        // PROPERTIES: ----------------------------------------------------------------------------

        public ItemType ItemType => _itemType;
        public string ItemKey => _itemKey;

        private string Label =>
            $"'Item type: {_itemType}',    'Item key: {(string.IsNullOrEmpty(_itemKey) ? "none" : _itemKey)}'";

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetItemKey(string itemKey) =>
            _itemKey = itemKey;
    }
}