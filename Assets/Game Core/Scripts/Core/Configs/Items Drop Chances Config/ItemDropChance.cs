using System;
using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Configs
{
    [Serializable]
    public struct ItemDropChance
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private ItemRarity _itemRarity;

        [SerializeField, ProgressBar(0, 100, ColorGetter = nameof(GetRarityColor))]
        private float _dropChance;

        // PROPERTIES: ----------------------------------------------------------------------------

        public ItemRarity ItemRarity => _itemRarity;
        public float DropChance => _dropChance;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Color GetRarityColor() =>
            ItemsRarityConfigMeta.GetRarityColorStatic(_itemRarity);
    }
}