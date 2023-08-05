using System;
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

        public ItemData(string itemID, int level) : this(itemID) =>
            _level = level;

        // MEMBERS: -------------------------------------------------------------------------------

        [BoxGroup(InfoGroup), SerializeField]
        private string _itemID = "item_id";

        [BoxGroup(InfoGroup), SerializeField, ReadOnly]
        [Tooltip("Unique key of the item.")]
        private string _itemKey;

        [BoxGroup(InfoGroup), SerializeField, Min(1)]
        private int _level = 1;

        [BoxGroup(StatsGroup), SerializeField, InlineProperty, HideLabel]
        private ItemStats _itemStats;

        // FIELDS: --------------------------------------------------------------------------------

        private const string InfoGroup = "Info";
        private const string StatsGroup = "Stats";

        // PROPERTIES: ----------------------------------------------------------------------------

        public string ItemID => _itemID;
        public string ItemKey => _itemKey;
        public int Level => _level;

        private string Label => $"'Item ID: {_itemID}',   'Level: {_level}'";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button(15), GUIColor(0.5f, 0.5f, 1f)]
        private void CopyKey() =>
            _itemKey.CopyToClipboard();

        [Button(15), GUIColor(1, 0.5f, 0.5f)]
        private void GenerateKey() =>
            _itemKey = GlobalUtilities.GetUniqueID();
    }
}