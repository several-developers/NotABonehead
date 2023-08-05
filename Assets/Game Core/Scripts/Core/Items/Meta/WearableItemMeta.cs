using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Items
{
    public class WearableItemMeta : ItemMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(ItemSettings)]
        [HorizontalGroup(Row, 80), VerticalGroup(RowLeft)]
        [PreviewField(60, ObjectFieldAlignment.Left), SerializeField, HideLabel, AssetsOnly]
        private Sprite _icon;

        [VerticalGroup(RowRight), SerializeField]
        private string _itemID = "item_id";
        
        [VerticalGroup(RowRight), SerializeField]
        private ItemType _itemType;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite Icon => _icon;
        public string ItemID => _itemID;
        public ItemType ItemType => _itemType;
        
        // FIELDS: --------------------------------------------------------------------------------

        private const string ItemSettings = "Wearable Item Settings";
        private const string Row = ItemSettings + "/Row";
        private const string RowLeft = Row + "/Left";
        private const string RowRight = Row + "/Right";

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
    }
}