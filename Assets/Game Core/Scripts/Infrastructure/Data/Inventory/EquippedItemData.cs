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

        public EquippedItemData(ItemType itemType, string itemKey) : this(itemType) =>
            _itemKey = itemKey;

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemType _itemType;

        [SerializeField]
        private string _itemKey;

        // PROPERTIES: ----------------------------------------------------------------------------

        private string Label =>
            $"'Item type: {_itemType}',    'Item key: {(string.IsNullOrEmpty(_itemKey) ? "none" : _itemKey)}'";
    }
}