using GameCore.Enums;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class ItemsDropChancesConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [InfoBox("Value out of bounce!", InfoMessageType.Error, visibleIfMemberName: "@_totalPercent != 100")]
        [InlineButton(nameof(CalculatePercent), "Recalculate")]
        [SerializeField]
        private float _totalPercent;

        [SerializeField, Space(5)]
        private ItemDropChance[] _itemsDropChances;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public float GetItemDropChance(ItemRarity itemRarity)
        {
            foreach (ItemDropChance itemDropChance in _itemsDropChances)
            {
                if (itemDropChance.ItemRarity == itemRarity)
                    return itemDropChance.DropChance;
            }

            return 0;
        }

        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        [OnInspectorInit]
        private void CalculatePercent()
        {
            _totalPercent = 0;
            
            foreach (ItemDropChance itemDropChance in _itemsDropChances)
                _totalPercent += itemDropChance.DropChance;
        }
    }
}