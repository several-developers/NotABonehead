using GameCore.Enums;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class ItemsRarityConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private ItemRarityConfig[] _itemRarityConfigs;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemRarityConfig GetItemRarityConfig(ItemRarity itemRarity)
        {
            foreach (ItemRarityConfig itemRarityConfig in _itemRarityConfigs)
            {
                if (itemRarityConfig.ItemRarity == itemRarity)
                    return itemRarityConfig;
            }

            return _itemRarityConfigs[0];
        }
        
        public static Color GetRarityColorStatic(ItemRarity itemRarity)
        {
            return itemRarity switch
            {
                ItemRarity.Common => new Color(r: 0.592f, g: 0.517f, b: 0.415f, a: 1f),
                ItemRarity.Rare => new Color(r: 0.372f, g: 0.803f, b: 0.231f, a: 1f),
                ItemRarity.Epic => new Color(r: 0.945f, g: 0.603f, b: 0.008f, a: 1f),
                ItemRarity.Legendary => new Color(r: 0.988f, g: 0.09f, b: 0.008f, a: 1f),
                ItemRarity.Mythic => new Color(r: 0.713f, g: 0.247f, b: 0.917f, a: 1f),
                _ => Color.gray
            };
        }

        public override string GetMetaCategory() =>
            EditorConstants.ConfigsCategory;
    }
}