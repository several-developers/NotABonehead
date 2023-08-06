using GameCore.Enums;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Items
{
    public class ItemMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(ItemSettings)]
        [HorizontalGroup(Row, 80), VerticalGroup(RowLeft)]
        [PreviewField(60, ObjectFieldAlignment.Left), SerializeField, HideLabel, AssetsOnly]
        private Sprite _icon;

        [VerticalGroup(RowRight), SerializeField]
        private string _itemID = "item_id";
        
        [VerticalGroup(RowRight), SerializeField]
        private string _itemName = "item_name";
        
        [VerticalGroup(RowRight), SerializeField]
        private ItemType _itemType;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite Icon => _icon;
        public string ItemID => _itemID;
        public string ItemName => _itemName;
        public ItemType ItemType => _itemType;
        
        // FIELDS: --------------------------------------------------------------------------------

        protected const string RowRight = Row + "/Right";
        
        private const string ItemSettings = "Wearable Item Settings";
        private const string Row = ItemSettings + "/Row";
        private const string RowLeft = Row + "/Left";
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override string GetMetaCategory() =>
            EditorConstants.ItemsCategory;
    }
}