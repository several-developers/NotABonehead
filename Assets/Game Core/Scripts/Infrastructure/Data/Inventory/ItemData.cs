using System;
using GameCore.Enums;
using GameCore.Items;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class ItemData
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemData() => GenerateKey();

        public ItemData(string itemID) : this() =>
            _itemID = itemID;

        public ItemData(string itemID, int level, ItemRarity itemRarity = ItemRarity.Common) : this(itemID) =>
            _itemStats = new ItemStats(itemRarity, level, health: 0, damage: 0, defense: 0);

        public ItemData(string itemID, ItemStats itemStats) : this(itemID) =>
            _itemStats = itemStats;

        // MEMBERS: -------------------------------------------------------------------------------

        [BoxGroup(InfoGroup), SerializeField]
        private string _itemID = "item_id";

        [BoxGroup(InfoGroup), SerializeField, ReadOnly]
        [Tooltip("Unique key of the item.")]
        private string _itemKey;

        [BoxGroup(StatsGroup), SerializeField, InlineProperty, HideLabel]
        private ItemStats _itemStats;

        // FIELDS: --------------------------------------------------------------------------------

        private const string InfoGroup = "Info";
        private const string StatsGroup = "Stats";

        // PROPERTIES: ----------------------------------------------------------------------------

        public string ItemID => _itemID;
        public string ItemKey => _itemKey;
        public ItemStats ItemStats => _itemStats;

        private string Label =>
            $"'Item ID: {_itemID}',    'Rarity: {_itemStats.Rarity}',   'Level: {_itemStats.Level}'";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetItemID(string itemID) =>
            _itemID = itemID;

        public void SetItemStats(ItemStats itemStats) =>
            _itemStats = itemStats;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button(15), GUIColor(0.5f, 0.5f, 1f)]
        private void CopyKey() =>
            _itemKey.CopyToClipboard();

        [Button(15), GUIColor(1, 0.5f, 0.5f)]
        private void GenerateKey() =>
            _itemKey = GlobalUtilities.GetUniqueID();
    }
}