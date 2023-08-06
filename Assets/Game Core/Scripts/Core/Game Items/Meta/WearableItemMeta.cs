using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Items
{
    public class WearableItemMeta : ItemMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [VerticalGroup(RowRight), SerializeField]
        private ItemType _itemType;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public ItemType ItemType => _itemType;
    }
}