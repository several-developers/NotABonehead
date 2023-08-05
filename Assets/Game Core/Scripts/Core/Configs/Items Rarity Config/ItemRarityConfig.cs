using System;
using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Configs
{
    [Serializable]
    public class ItemRarityConfig
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemRarity _itemRarity;
        
        [SerializeField]
        private Color _rarityColor = Color.white;

        [SerializeField, Required]
        private Sprite _rarityFrame;

        // PROPERTIES: ----------------------------------------------------------------------------

        public ItemRarity ItemRarity => _itemRarity;
        public Color RarityColor => _rarityColor;
        public Sprite RarityFrame => _rarityFrame;
    }
}